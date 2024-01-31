using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointToMiddle : MonoBehaviour
{
    public RectTransform uiElement;

    void Update()
    {
        // Get the center position of the screen in world space
        Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));

        // Get the direction from the UI element to the center of the screen
        Vector3 directionToCenter = centerScreen - uiElement.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;

        // Apply the rotation to the UI element
        uiElement.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + -45f));
        
    }
}
