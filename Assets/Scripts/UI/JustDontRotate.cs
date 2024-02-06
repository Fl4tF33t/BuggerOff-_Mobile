using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDontRotate : MonoBehaviour
{
    public RectTransform uiElement;

    private void Start()
    {
        uiElement = GetComponent<RectTransform>();
    }
    void Update()
    {
        //uiElement.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }
}
