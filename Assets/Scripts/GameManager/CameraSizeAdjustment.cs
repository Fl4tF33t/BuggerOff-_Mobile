using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSizeAdjustment : MonoBehaviour
{
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        float screenRatio = (float)Screen.height / (float)Screen.width;
        float targetRatio = 9f / 16f;
        float heightAdjusted;

        if(screenRatio <= targetRatio)
        {
            cam.orthographicSize = 5.1f;
        }
        else
        {
            heightAdjusted = 9 / ((float)Screen.width / (float)Screen.height);
            cam.orthographicSize = heightAdjusted;
        }
    }
}
