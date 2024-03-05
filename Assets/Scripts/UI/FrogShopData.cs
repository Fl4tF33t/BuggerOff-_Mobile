using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FrogShopData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private FrogSO frogSO;
    private Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        //change the image of the shop icon
        ShopManager.Instance.OnShopIconChange(frogSO.visualSO.userInterface.UIShopSprite);
    }

    private void Awake()
    {
        image = GetComponent<Image>();

        InitializeShopUI();
    }

    private void InitializeShopUI()
    {
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
    }
}
