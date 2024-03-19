using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IPointerClickHandler
{
    FrogSO frogso;

    private Image image;
    private GameObject selectedFrogPrefab;
    [SerializeField]
    private GameObject wheel;
    private Animator animator;


    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        animator = GetComponentInParent<Animator>();
        //frogso.visualSO.userInterface.
    }

    private void Start()
    {
        //InputManager.Instance.OnTouchTap += (obj) => { SetShopOnOff(true); };
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
        InputManager.Instance.OnTouchPressCanceled += Instance_OnTouchPressCanceled;
        wheel.GetComponentInChildren<WheelLogic>().OnPlaceFrog += ShopManager_OnPlaceFrog;
    }

    private void Instance_OnTouchTap(Vector2 obj)
    {
        SetShopOnOff(true);
        // Raycast from mouse position to the ground to get the point where the object is released
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(obj.x, obj.y, 10f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Testing");
            // Check if there is NavMesh at the point where the object is released
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, 1))
            {
                Debug.Log("NavMesh available at point: " + navHit.position);
                Debug.Log(navHit.mask);
                // Do something if NavMesh is available
            }
            else
            {
                Debug.Log("No NavMesh available at point");
                // Do something if NavMesh is not available
            }
        }
    }

    

    public void SetShopOnOff(bool state)
    {
        animator.SetBool("OnStoreClick", state);
        image.raycastTarget = state;
    }

    private void ShopManager_OnPlaceFrog(FrogSO obj)
    {
        SetShopOnOff(true);
        selectedFrogPrefab = Instantiate(obj.prefab, new Vector3 (6.2f, 0, -2.5f), Quaternion.identity);

        InputManager.Instance.OnTouchInput += Instance_OnTouchInput;
    }

    private void Instance_OnTouchInput(Vector2 obj)
    {
        // Convert screen position to world position
        Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
        selectedFrogPrefab.transform.position = targetWorldPosition;
    }
    private void Instance_OnTouchPressCanceled(Vector2 obj)
    {
        if(selectedFrogPrefab != null)
        {           
            // Raycast from mouse position to the ground to get the point where the object is released
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(obj.x, obj.y, 10f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Debug.Log("Testing");
                // Check if there is NavMesh at the point where the object is released
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 0.1f, NavMesh.AllAreas))
                {
                    Debug.Log("NavMesh available at point: " + navHit.position);
                    Debug.Log("NavMesh area mask: " + navHit.mask);
                    // Do something if NavMesh is available
                }
                else
                {
                    Debug.Log("No NavMesh available at point");
                    // Do something if NavMesh is not available
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetShopOnOff(false);
    }
} 
