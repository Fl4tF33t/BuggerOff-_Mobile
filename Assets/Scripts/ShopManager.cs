using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>, IPointerClickHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public Action<FrogSO> OnFrogChange;
    //private Canvas canvas;
    //public Action<bool> OnSetShopIcon;
    private Image image;
    //private bool isBeingDragged;

    //private GameObject selectedFrogPrefab;
    [SerializeField]
    private GameObject wheel;
    private Animator wheelAnimator;

    

    protected override void Awake()
    {
        base.Awake();
    //    canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();
        //wheel = transform.GetChild(0).gameObject;
       wheelAnimator = wheel.GetComponentInParent<Animator>();
    }

    private void Start()
    {
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
        
        //OnSetShopIcon = SetShopOnOff;
    }

    public void SetShopOnOff(bool state)
    {
        Color col = image.color;
        if(!state)
        {
            //change the visibility of the button
            col.a = 0f;
            image.color = col;
            wheelAnimator.SetBool("OnStoreClick", state);

        }
        else
        {
            //change the visibility of the button
            col.a = 1f;
            image.color = col;
            wheelAnimator.SetBool("OnStoreClick", state);

        }

        image.raycastTarget = state;
    }
    private void Instance_OnTouchTap(object sender, InputManager.OnTouchTapEventArgs e)
    {
        //wheel.SetActive(false);

        //StartCoroutine(ScaleOverTime(wheel.transform, 0f, 0.3f));
        SetShopOnOff(true);
    }

    //private void SetFrog(FrogSO frogSO) 
    //{
    //    image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
    //    selectedFrogPrefab = frogSO.prefab;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (!isBeingDragged)
        //{
        //    wheel.SetActive(!wheel.activeSelf); 
        //}
        //wheel.SetActive(!wheel.activeSelf);
        //StartCoroutine(ScaleOverTime(wheel.transform, 1f, 0.3f));
        //wheel.SetActive(true);
        SetShopOnOff(false);
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    isBeingDragged = true;
    //    wheel.SetActive(false);
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    image.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    //check if can place. otherwise it stays
    //}

    //public IEnumerator ScaleOverTime(Transform targetTransform, float targetScale, float duration)
    //{
    //    Vector3 originalScale = transform.localScale;
    //    Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);

    //    float currentTime = 0f;

    //    while (currentTime <= duration)
    //    {
    //        float t = currentTime / duration;
    //        targetTransform.localScale = Vector3.Lerp(originalScale, targetScaleVector, t);
    //        currentTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    if (image.raycastTarget == true)
    //    {
    //        //ShopManager.Instance.OnFrogChange(frogSO);
    //    }
    //    targetTransform.localScale = targetScaleVector;
    //}
} 
