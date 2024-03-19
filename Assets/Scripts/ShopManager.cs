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
    public GameObject selectedFrogPrefab;
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
        InputManager.Instance.OnTouchTap += (obj) => { SetShopOnOff(true); };
        InputManager.Instance.OnTouchPressCanceled += Instance_OnTouchPressCanceled;
        wheel.GetComponentInChildren<WheelLogic>().OnPlaceFrog += ShopManager_OnPlaceFrog;
    }

    public void SetShopOnOff(bool state)
    {
        animator.SetBool("OnStoreClick", state);
        image.raycastTarget = state;
    }

    private void ShopManager_OnPlaceFrog(FrogSO obj)
    {
        SetShopOnOff(true);
        frogso = obj;
        selectedFrogPrefab = Instantiate(obj.prefab, new Vector3 (6.2f, 0, -2.5f), Quaternion.identity);

        InputManager.Instance.OnTouchInput += Instance_OnTouchInput;
    }

    private void Instance_OnTouchInput(Vector2 obj)
    {
        // Convert screen position to world position
        if (selectedFrogPrefab != null)
        {
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            selectedFrogPrefab.transform.position = targetWorldPosition; 
        }
    }
    private void Instance_OnTouchPressCanceled(Vector2 obj)
    {
        if(selectedFrogPrefab != null)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(point, out navHit, 0.1f, NavMesh.AllAreas))
            {
                Debug.Log("Testing");
                switch (navHit.mask)
                {
                    case 1:
                        //ground frogs
                        if (!frogso.logicSO.isWaterFrog)
                        {                            
                            selectedFrogPrefab.GetComponent<FrogBrain>().SpawnFrog();
                            selectedFrogPrefab = null;
                            Debug.Log("Place Ground frog");
                        }
                        break;
                    case 8:
                        //path for bugs
                        break;
                    case 16:
                        //water frogs
                        if (frogso.logicSO.isWaterFrog)
                        {                            
                            selectedFrogPrefab.GetComponent<FrogBrain>().SpawnFrog();
                            selectedFrogPrefab = null;
                            Debug.Log("Place Water frog");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("No NavMesh available at point");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetShopOnOff(false);
    }
} 
