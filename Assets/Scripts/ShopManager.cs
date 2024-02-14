using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour, IPointerDownHandler
{
    public GameObject wheel;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (wheel.activeSelf)
        {
            wheel.SetActive(false);
        }
        else
        {
            wheel.SetActive(true);
        }
    }
}
