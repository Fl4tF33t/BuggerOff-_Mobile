using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopButton : UIButtonManager
{
    private void Start()
    {
        Image image = GetComponent<Image>();
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the shop, i assume selection of that frog. Need to establish if not enough money, can you select a frog
    }
}
