using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ShopManager : Singleton<ShopManager>, IPointerClickHandler
{
    public Action<bool> OnSetShopOnOff;

    private FrogSO frogSO;
    private GameObject selectedFrogPrefab;
    private Vector3 prefabPos;

    [SerializeField]
    private GameObject wheel;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        wheel.GetComponentInChildren<WheelLogic>().OnPlaceFrog += ShopManager_OnPlaceFrog;

        OnSetShopOnOff = (state) => { animator.SetBool("OnStoreClick", state); };

        InputManager.Instance.OnTouchTap += (obj) => { OnSetShopOnOff(true); };
        InputManager.Instance.OnTouchPressStarted += Instance_OnTouchPressStarted;
        InputManager.Instance.OnTouchPressCanceled += Instance_OnTouchPressCanceled;
    }

    private void ShopManager_OnPlaceFrog(FrogSO obj)
    {
        OnSetShopOnOff(true);

        frogSO = obj;
        selectedFrogPrefab = Instantiate(obj.prefab, new Vector3(6.2f, 0, -2.5f), Quaternion.identity);

        InputManager.Instance.OnTouchInput += Instance_OnTouchInput;
    }

    private void Instance_OnTouchPressStarted(Vector2 obj)
    {
        if (selectedFrogPrefab != null)
        {
            //set the position of the prefab gameobject
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            prefabPos -= targetWorldPosition;
            selectedFrogPrefab.transform.position = targetWorldPosition + prefabPos;
            selectedFrogPrefab.GetComponent<FrogBrain>().ChangeColor(Color.red);
        }
    }

    private void Instance_OnTouchInput(Vector2 obj)
    {
        // Convert screen position to world position
        if (selectedFrogPrefab != null)
        { 
            //set the position of the prefab gameobject
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            selectedFrogPrefab.transform.position = targetWorldPosition + prefabPos;

            //set the color of the prefab game object
            Color col = IsPlacable(selectedFrogPrefab.transform.position) ? Color.green : Color.red;
            selectedFrogPrefab.GetComponent<FrogBrain>().ChangeColor(col);
        }
    }
    private void Instance_OnTouchPressCanceled(Vector2 obj)
    {
        if(selectedFrogPrefab != null)
        {
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
            selectedFrogPrefab.transform.position = targetWorldPosition + prefabPos;
            prefabPos = selectedFrogPrefab.transform.position;
            if (IsPlacable(prefabPos))
            {
                selectedFrogPrefab.GetComponent<FrogBrain>().SpawnFrog();
                frogSO = null;
                selectedFrogPrefab = null;
                prefabPos = Vector3.zero;

                InputManager.Instance.OnTouchInput -= Instance_OnTouchInput;
            }
        }
    }
    private bool IsPlacable(/*Vector2 obj, */Vector3 pos)
    {
        //locate the postion in world space
        //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(obj.x, obj.y, 10f));
        NavMeshHit navHit;

        //check if it is in a navmesh area
        if (NavMesh.SamplePosition(pos, out navHit, 0.1f, NavMesh.AllAreas))
        {
            //check if there are other game objects that would collide withion the same area
            Collider[] colliders = Physics.OverlapSphere(navHit.position, 0.2f);
            bool res;

            switch (navHit.mask)
            {
                case 1:
                    //ground frogs
                    res = !frogSO.logicSO.isWaterFrog && colliders.Length == 0;
                    return res;
                case 8:
                    //path for bugs
                    return false;
                case 16:
                    //water frogs
                    res = frogSO.logicSO.isWaterFrog && colliders.Length == 0;
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
        if(selectedFrogPrefab != null)
        {
            Destroy(selectedFrogPrefab);
            selectedFrogPrefab = null;
            prefabPos = Vector3.zero;
            InputManager.Instance.OnTouchInput -= Instance_OnTouchInput;
        }
    }
} 
