using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragAndDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            UIDragAndDropManager.Instance.rectTransform = null;
        }
    }
}
