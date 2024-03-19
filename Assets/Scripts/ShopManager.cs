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
    public Action<bool> OnSetShopOnOff;

    private FrogSO frogSO;
    private GameObject selectedFrogPrefab;

    [SerializeField]
    private GameObject wheel;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        wheel.GetComponentInChildren<WheelLogic>().OnPlaceFrog += ShopManager_OnPlaceFrog;

        InputManager.Instance.OnTouchTap += (obj) => { OnSetShopOnOff(true); };
        InputManager.Instance.OnTouchPressCanceled += Instance_OnTouchPressCanceled;

        OnSetShopOnOff = (state) => { animator.SetBool("OnStoreClick", state); };
    }

    private void ShopManager_OnPlaceFrog(FrogSO obj)
    {
        OnSetShopOnOff(true);
        frogSO = obj;
        selectedFrogPrefab = Instantiate(obj.prefab, new Vector3 (6.2f, 0, -2.5f), Quaternion.identity);

        InputManager.Instance.OnTouchInput += Instance_OnTouchInput;
    }

    private void Instance_OnTouchInput(Vector2 obj)
    {
        // Convert screen position to world position
        if (selectedFrogPrefab != null)
        { 
            //set the position of the prefab gameobject
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            selectedFrogPrefab.transform.position = targetWorldPosition;

            //set the color of the prefab game object
            Color col = IsPlacable(obj) ? Color.green : Color.red;
            selectedFrogPrefab.GetComponent<FrogBrain>().ChangeColor(col);
        }
    }
    private void Instance_OnTouchPressCanceled(Vector2 obj)
    {
        if(selectedFrogPrefab != null)
        {
            if (IsPlacable(obj))
            {
                selectedFrogPrefab.GetComponent<FrogBrain>().SpawnFrog();
                frogSO = null;
                selectedFrogPrefab = null;
                InputManager.Instance.OnTouchInput -= Instance_OnTouchInput;
            }
        }
    }
    private bool IsPlacable(Vector2 obj)
    {
        //locate the postion in world space
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
        NavMeshHit navHit;

        //check if it is in a navmesh area
        if (NavMesh.SamplePosition(point, out navHit, 0.1f, NavMesh.AllAreas))
        {
            //check if there are other game objects that would collide withion the same area
            Collider[] colliders = Physics.OverlapSphere(navHit.position, 0.2f);
            bool res;

            switch (navHit.mask)
            {
                case 1:
                    //ground frogs
                    res = !frogSO.logicSO.isWaterFrog && colliders.Length == 0 ? true : false;
                    return res;
                case 8:
                    //path for bugs
                    return false;
                case 16:
                    //water frogs
                    res = frogSO.logicSO.isWaterFrog && colliders.Length == 0 ? true : false;
                    return res;
                default:
                    return false;
            }
        }
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSetShopOnOff(false);
    }
} 
