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

    //drag and drop
    internal RectTransform rectTransform;
    private Canvas canvas;
    internal CanvasGroup canvasGroup;


    private void Awake()
    {
        //sets the image of the button to the frog sprite, according to the FrogSO that is attached to the button
        image = GetComponent<Image>();
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
        
        //finds the text that is used to show the info of the frog
        text = GameObject.Find("ShopUIHoldInfo").GetComponent<TextMeshProUGUI>();

        //drag and drop
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        UIDragAndDropManager.Instance.uiShopButton = this;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("begin drag");    
    }
    public override void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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
