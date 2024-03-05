using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IPointerDownHandler
{
    public Action<Sprite> OnShopIconChange;
    [SerializeField]
    private GameObject wheel;
    [SerializeField]
    private Sprite shopIcon;
    private Image image;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
    }

    private void Instance_OnTouchTap(object sender, InputManager.OnTouchTapEventArgs e)
    {
        wheel.SetActive(false);
        OnShopIconChange(shopIcon);
    }

    private void Start()
    {
        OnShopIconChange = (sprite) => { image.sprite = sprite; };
        OnShopIconChange(shopIcon);
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
