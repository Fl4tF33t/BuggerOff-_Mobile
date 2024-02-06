using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpgradeCanvasPointToMiddle : MonoBehaviour
{
    Vector3 centerScreen;
    RectTransform rectTransform;

    RectTransform[] rectTransforms = null;
    List<RectTransform> fixedRectTransform = new List<RectTransform>();
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransforms = GetComponentsInChildren<RectTransform>();
    }

    private void Start()
    {
        // Get the center position of the screen in world space
        centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));

        //make a private list of the same obj that have an image
        foreach (RectTransform t in rectTransforms)
        {
            //use a try get component to make a specific transform in the array rotate
            if (t.TryGetComponent(out Image image))
            {
                fixedRectTransform.Add(t);
            }
        }
    }

    private void Update()
    {
        // Get the direction from the UI element to the center of the screen
        Vector3 directionToCenter = centerScreen - rectTransform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;

        // Apply the rotation to the UI element
        rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + -45f));

        //keep the text and the images of the UI static and not moving
        foreach (RectTransform t in fixedRectTransform)
        {
            t.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
}
