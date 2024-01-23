using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragAndDropManager : Singleton<UIDragAndDropManager>, IDragHandler, IPointerClickHandler
{
    public RectTransform rectTransform;
    private Canvas canvas;

    protected override void Awake()
    {
        base.Awake();
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }
}
