using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FrogShopData : MonoBehaviour
{
    //Initializtion of the frog information
    private Image image;
    public Action<FrogSO> OnSetFrogSO;

    private void Awake()
    {
        image = GetComponent<Image>();

        OnSetFrogSO = SetFrogSO;
    }
    private void SetFrogSO(FrogSO data)
    {
        image.sprite = data.visualSO.userInterface.UIShopSprite;
    }
}
