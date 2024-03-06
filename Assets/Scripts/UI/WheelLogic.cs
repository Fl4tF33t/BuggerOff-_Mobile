using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelLogic : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Movement of the wheel
    public event Action<float> OnWheelIconChange;
    public event Action OnButtonScroll;
    private Vector2 dragStartPos;
    private Vector2 delta;
    private int index;

    //Visuals of the buttons
    [SerializeField]
    private FrogSO[] frogPool = new FrogSO[8];
    [SerializeField]
    private Image[] buttonIcons = new Image[3];
    private FrogShopData[] frogShopData = new FrogShopData[3];

    private void Start()
    {
        for (int i = 0; i < buttonIcons.Length;  i++)
        {
            frogShopData[i] = transform.GetChild(i).transform.GetChild(0).GetComponent<FrogShopData>();
            frogShopData[i].frogSO = frogPool[i];
            frogShopData[i].OnSetImage();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnWheelIconChange?.Invoke(0.5f);
        dragStartPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        delta = eventData.position - dragStartPos;
        dragStartPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool isSwipeUp = delta.x > 0 || delta.y > 0 ? true : false;
        int currentIndex;

        if(isSwipeUp)
        {
            index++;
            index = index % frogPool.Length;
            currentIndex = index;
            for (int i = 0; i < buttonIcons.Length; i++)
            {
                frogShopData[i].frogSO = frogPool[currentIndex];
                frogShopData[i].OnSetImage();
                //buttonIcons[i].sprite = frogPool[currentIndex].visualSO.userInterface.UIShopSprite;
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
                frogShopData[i].frogSO = frogPool[currentIndex];
                frogShopData[i].OnSetImage();
                //buttonIcons[i].sprite = frogPool[currentIndex].visualSO.userInterface.UIShopSprite;
                currentIndex++;
                currentIndex = currentIndex % frogPool.Length;
            }
        }
        OnWheelIconChange?.Invoke(1f);
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
            frogShopData[i].frogSO = frogPool[currentIndex];
            frogShopData[i].OnSetImage();
            //buttonIcons[i].sprite = frogPool[currentIndex].visualSO.userInterface.UIShopSprite;
            currentIndex++;
            currentIndex = currentIndex % frogPool.Length;
        }
    }

    public void OnScrollDown()
    {
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
            frogShopData[i].frogSO = frogPool[currentIndex];
            frogShopData[i].OnSetImage();
            //buttonIcons[i].sprite = frogPool[currentIndex].visualSO.userInterface.UIShopSprite;
            currentIndex++;
            currentIndex = currentIndex % frogPool.Length;
        }
    }
}
