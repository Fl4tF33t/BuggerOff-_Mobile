using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIShopButton : UIButtonManager
{
    private Image image;
    private TextMeshProUGUI text;

    private void Awake()
    {
        //sets the image of the button to the frog sprite, according to the FrogSO that is attached to the button
        image = GetComponent<Image>();
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
        
        //finds the text that is used to show the info of the frog
        text = GameObject.Find("ShopUIHoldInfo").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the shop, i assume selection of that frog. Need to establish if not enough money, can you select a frog
    }

    protected override void ActivatedHoldDown()
    {
        text.text = frogSO.visualSO.userInterface.UIShopTextInfo;
        text.gameObject.SetActive(true);
    }
    
    protected override void DeactivatedHoldDown()
    {
        text.gameObject.SetActive(false);
    }
}
