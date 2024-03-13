using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FrogShopData : MonoBehaviour
{
    private WheelLogic wheelLogic;
    public float scaleSpeed = .3f;

    //Initializtion of the frog information
    private FrogSO frogSO;
    private Image image;
    public Action<FrogSO> OnSetFrogSO;

    private void Awake()
    {
        wheelLogic = GetComponentInParent<WheelLogic>();
        image = GetComponent<Image>();

        OnSetFrogSO = SetFrogSO;
    }

    private void Start()
    {
        wheelLogic.OnWheelIconChange += (scale) => { StartCoroutine(ScaleOverTime(scale, scaleSpeed)); };
        wheelLogic.OnButtonScroll += () => { StartCoroutine(ShrinkAndEnlarge()); };
    }
    private void SetFrogSO(FrogSO data)
    {
        frogSO = data;
        image.sprite = frogSO.visualSO.userInterface.UIShopSprite;
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
