using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelLogic : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Movement of the wheel
    public event Action<float> OnWheelIconChange;
    public event Action<FrogSO> OnPlaceFrog;
    public event Action OnButtonScroll;
    private int index;

    //Visuals of the buttons
    [SerializeField]
    private FrogSO[] frogPool = new FrogSO[8];
    private Image[] buttonIcons = new Image[3];
    private FrogShopData[] frogShopData = new FrogShopData[3];

    //EventSystem of the UI
    private EventSystem eventSystem;
    private bool isSpinning;

    private void Awake()
    {
        eventSystem = EventSystem.current;

        //Initialize the shop icons
        for (int i = 0; i < buttonIcons.Length;  i++)
        {
            frogShopData[i] = transform.GetChild(i).transform.GetChild(0).GetComponent<FrogShopData>();
            frogShopData[i].OnSetFrogSO(frogPool[i]);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        switch (results.Count)
        {
            case 0:
                break;
            case 1:                
                isSpinning = true;
                OnWheelIconChange?.Invoke(0.5f);
                break;
            case 2:
                int frogPoolIndex = (index + 1) % frogPool.Length;
                isSpinning = false;
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
            bool isSwipeUp = eventData.delta.x > 0 || eventData.delta.y > 0 ? true : false;
            int currentIndex;

            if (isSwipeUp)
            {
                index++;
                index = index % frogPool.Length;
                currentIndex = index;
                for (int i = 0; i < buttonIcons.Length; i++)
                {
                    frogShopData[i].OnSetFrogSO(frogPool[currentIndex]);
                    currentIndex++;
                    currentIndex = currentIndex % frogPool.Length;
                }
            }
            else
            {
                index--;
                if (index < 0)
                {
                    index = frogPool.Length - 1;
                }
                currentIndex = index;
                for (int i = 0; i < buttonIcons.Length; i++)
                {
                    frogShopData[i].OnSetFrogSO(frogPool[currentIndex]);
                    currentIndex++;
                    currentIndex = currentIndex % frogPool.Length;
                }
            }
            OnWheelIconChange?.Invoke(1f);
            //isSpinning = false;
        }
    }

    public void OnScrollUp()
    {
        OnButtonScroll?.Invoke();

        int currentIndex;
        index++;
        index = index % frogPool.Length;
        currentIndex = index;
        for (int i = 0; i < buttonIcons.Length; i++)
        {
            frogShopData[i].OnSetFrogSO(frogPool[currentIndex]);
            currentIndex++;
            currentIndex = currentIndex % frogPool.Length;
        }
    }

    public void OnScrollDown()
    {
        //Here for the animations!
        OnButtonScroll?.Invoke();

        int currentIndex;
        index--;
        if (index < 0)
        {
            index = frogPool.Length - 1;
        }
        currentIndex = index;
        for (int i = 0; i < buttonIcons.Length; i++)
        {
            frogShopData[i].OnSetFrogSO(frogPool[currentIndex]);
            currentIndex++;
            currentIndex = currentIndex % frogPool.Length;
        }
    }

}
