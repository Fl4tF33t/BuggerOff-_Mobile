using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<FrogSO> OnFrogChange;
    private Canvas canvas;
    private Image image;
    private bool isBeingDragged;

    private GameObject selectedFrogPrefab;
    [SerializeField]
    private GameObject wheel;
    

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
        OnFrogChange = SetFrog;
    }
    private void Instance_OnTouchTap(object sender, InputManager.OnTouchTapEventArgs e)
    {
        wheel.SetActive(false);
    }

    private void SetFrog(FrogSO frogSO) 
    {
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
        selectedFrogPrefab = frogSO.prefab;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isBeingDragged)
        {
            wheel.SetActive(!wheel.activeSelf); 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isBeingDragged = true;
        wheel.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        image.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //check if can place. otherwise it stays
    }
} 
