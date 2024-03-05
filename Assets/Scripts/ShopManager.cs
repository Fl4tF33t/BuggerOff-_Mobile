using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IPointerDownHandler
{
    public Action<Sprite> OnShopIconChange;

    private GameObject wheel;
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        wheel = transform.GetChild(0).gameObject;
        OnShopIconChange = (sprite) => { image.sprite = sprite; };
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (wheel.activeSelf)
        {
            wheel.SetActive(false);
        }
        else
        {
            wheel.SetActive(true);
        }
    }
}
