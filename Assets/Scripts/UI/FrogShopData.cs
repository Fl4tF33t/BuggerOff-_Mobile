using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FrogShopData : MonoBehaviour, IPointerClickHandler
{
    private WheelLogic wheelLogic;
    public float scaleSpeed = .3f;
    [HideInInspector]
    public FrogSO frogSO;
    private Image image;
    public Action OnSetImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        //change the image of the shop icon
        ShopManager.Instance.OnFrogChange(frogSO);
    }

    private void Awake()
    {
        wheelLogic = GetComponentInParent<WheelLogic>();
        image = GetComponent<Image>();

        OnSetImage = () => { image.sprite = frogSO.visualSO.userInterface.UIShopSprite; };
        //InitializeShopUI();
    }

    private void Start()
    {
        wheelLogic.OnWheelIconChange += WheelLogic_OnWheelIconChange;
        wheelLogic.OnButtonScroll += WheelLogic_OnButtonScroll;
    }

    private void WheelLogic_OnButtonScroll()
    {
        StartCoroutine(ShrinkAndEnlarge());
    }

    private void WheelLogic_OnWheelIconChange(float scale)
    {
        StartCoroutine(ScaleOverTime(scale, scaleSpeed));
    }

    private IEnumerator ShrinkAndEnlarge()
    {
        yield return StartCoroutine(ScaleOverTime(0.5f, scaleSpeed));
        yield return StartCoroutine(ScaleOverTime(1f, scaleSpeed));
    }

    private IEnumerator ScaleOverTime(float targetScale, float duration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);

        float currentTime = 0f;

        while (currentTime <= duration)
        {
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(originalScale, targetScaleVector, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScaleVector;
    }
}
