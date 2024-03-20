using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WheelLogic : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Movement of the wheel
    public event Action<float> OnWheelIconChange;
    public event Action<FrogSO> OnPlaceFrog;

    public event Action OnButtonScroll;

    //Visuals of the buttons
    [SerializeField]
    private FrogSO[] frogPool = new FrogSO[8];
    private FrogShopData[] frogShopData;
    private int frogPoolIndex;
    private TextMeshProUGUI priceText;

    //EventSystem of the UI
    private EventSystem eventSystem;
    private bool isSpinning;

    private void Awake()
    {
        priceText = GetComponentInChildren<TextMeshProUGUI>();

        //Initialize the shop icons
        frogShopData = GetComponentsInChildren<FrogShopData>();
        for (int i = 0; i < frogShopData.Length; i++)
        {
            frogShopData[i].OnSetFrogSO(frogPool[i]);
        }

        SetPriceText(frogPoolIndex);

        eventSystem = EventSystem.current;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        switch (results.Count)
        {
            case 1:                
                isSpinning = true;
                OnWheelIconChange?.Invoke(0.5f);
                break;
            case 2:
                isSpinning = false;
                int frogPoolIndex = (this.frogPoolIndex + 1) % frogPool.Length;
                OnPlaceFrog?.Invoke(frogPool[frogPoolIndex]);
                break;
            default:
                break;
        }
    }

    //need to have the I drag even though it is empty to make the drag start/end to work
    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSpinning)
        {
            bool isSwipeUp = eventData.delta.y > 0;
            if (isSwipeUp)
            {
                frogPoolIndex++;
                frogPoolIndex = frogPoolIndex % frogPool.Length;
                SetPriceText(frogPoolIndex);
                SetFrogShopData();
            }
            else
            {
                frogPoolIndex--;
                if (frogPoolIndex < 0)
                {
                    frogPoolIndex = frogPool.Length - 1;                    
                }
                SetPriceText(frogPoolIndex);
                SetFrogShopData();
            }
            OnWheelIconChange?.Invoke(1f);
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
        int priceTextIndex = index++ % frogShopData.Length;
        priceText.text = frogPool[priceTextIndex].logicSO.cost.ToString();
    }

    public void OnScrollUp()
    {
        OnButtonScroll?.Invoke();

        frogPoolIndex++;
        frogPoolIndex = frogPoolIndex % frogPool.Length;

        SetPriceText(frogPoolIndex);
        SetFrogShopData();
    }

    public void OnScrollDown()
    {
        //Here for the animations!
        OnButtonScroll?.Invoke();

        frogPoolIndex--;
        if (frogPoolIndex < 0)
        {
            frogPoolIndex = frogPool.Length - 1;
        }

        SetPriceText(frogPoolIndex);
        SetFrogShopData();
    }

}
