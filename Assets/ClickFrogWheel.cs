using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class ClickFrogWheel : MonoBehaviour, IPointerClickHandler
{

    public event Action OnClickButton;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickButton?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
