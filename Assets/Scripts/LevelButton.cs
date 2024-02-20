using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerDownHandler
{
    Button easy;
    Button normal;
    Button hard;

    private void Awake()
    {
        easy = transform.Find("Easy").GetComponent<Button>();
        normal = transform.Find("Normal").GetComponent<Button>();
        hard = transform.Find("Hard").GetComponent<Button>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        easy.gameObject.SetActive(true);
        normal.gameObject.SetActive(true);
        hard.gameObject.SetActive(true);
    }
}
