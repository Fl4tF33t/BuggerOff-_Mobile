using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogShopData : MonoBehaviour
{
    [SerializeField]
    private FrogSO frogSO;
    private Image image;

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
