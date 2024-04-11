using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelManager : Singleton<WheelManager>, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Movement of the wheel
    //public event Action<int> OnWheelIconChange;
    public event Action<FrogSO, int> OnPlaceFrog;

    //public event Action OnButtonScroll;
    public event Action<string, int> OnWheelAnim;

    public Action SetPrice;

    //Visuals of the buttons
    private FrogSO[] frogPool;
    private FrogShopData[] frogShopData;
    private int frogPoolIndex;
    private TextMeshProUGUI priceText;

    //EventSystem of the UI
    private EventSystem eventSystem;
    private bool isSpinning;
    private Vector2 pos;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        priceText = GetComponentInChildren<TextMeshProUGUI>();
        frogShopData = GetComponentsInChildren<FrogShopData>();
        animator = GetComponentInParent<Animator>();
        eventSystem = EventSystem.current;
    }
    private void Start()
    {
        frogPool = ShopManager.Instance.frogPool;
        for (int i = 0; i < frogShopData.Length; i++)
        {
            frogShopData[i].OnSetFrogSO(frogPool[i]);
        }

        SetPriceText(frogPoolIndex);
        SetPrice = () => { SetPriceText(frogPoolIndex); };
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        bool animatorStateShopUP = animator.GetCurrentAnimatorStateInfo(0).IsTag("Open");
        Debug.Log("Animator state: " + animatorStateShopUP);
        if (!animatorStateShopUP)
        {
            switch (results.Count)
            {
                case 1:                
                    isSpinning = true;
                    pos = eventData.position;
                    break;
                case 2:
                    isSpinning = false;
                    int frogPoolIndex = (this.frogPoolIndex + 1) % frogPool.Length;
                    
                    if (GameManager.Instance.BugBits > frogPool[frogPoolIndex].logicSO.cost * NumberOfFrogs(frogPoolIndex))
                    {
                        OnPlaceFrog?.Invoke(frogPool[frogPoolIndex], NumberOfFrogs(frogPoolIndex));
                    }
                    break;
                default:
                    break;
            }
        }
        else isSpinning = false;
    }

    //need to have the I drag even though it is empty to make the drag start/end to work
    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSpinning)
        {
            bool isSwipeUp = pos.y < eventData.position.y;

            if (isSwipeUp)
            {
                OnWheelAnim?.Invoke("Up", frogPoolIndex);

                frogPoolIndex = (frogPoolIndex + 1) % frogPool.Length;
                SetPriceText(frogPoolIndex);
                SetFrogShopData();
                
            }
            else
            {
                OnWheelAnim?.Invoke("Down", frogPoolIndex);

                frogPoolIndex--;
                if (frogPoolIndex < 0)
                {
                    frogPoolIndex = frogPool.Length - 1;                    
                }
                SetPriceText(frogPoolIndex);
                SetFrogShopData();
            }
        }
    }

    private void SetFrogShopData()
    {
        int currentIndex = frogPoolIndex;
        for (int i = 0; i < frogShopData.Length; i++)
        {
            frogShopData[i].OnSetFrogSO(frogPool[currentIndex]);
            currentIndex++;
            currentIndex = currentIndex % frogPool.Length;
        }
    }

    private void SetPriceText(int index)
    {
        int priceTextIndex = ++index % frogShopData.Length;
        priceText.text = (frogPool[priceTextIndex].logicSO.cost * NumberOfFrogs(priceTextIndex)).ToString();
    }
    private int NumberOfFrogs(int index)
    {
        int num = 1;
        FrogBrain[] objects = FindObjectsOfType<FrogBrain>(); // Find all GameObjects in the scene

        foreach (FrogBrain obj in objects)
        {
            if (obj.frogSO.name == frogPool[index].name)
            {
                num++;
            }
        } 
        return num;
    }

    public void OnScrollUp()
    {
        //OnButtonScroll?.Invoke();
        OnWheelAnim?.Invoke("Up", frogPoolIndex);

        frogPoolIndex = (frogPoolIndex + 1) % frogPool.Length;

        SetPriceText(frogPoolIndex);
        SetFrogShopData();
    }

    public void OnScrollDown()
    {
        //Here for the animations!
        OnWheelAnim?.Invoke("Down", frogPoolIndex);

        frogPoolIndex--;
        if (frogPoolIndex < 0)
        {
            frogPoolIndex = frogPool.Length - 1;
        }

        SetPriceText(frogPoolIndex);
        SetFrogShopData();
    }

}
