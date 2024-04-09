using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIUpgradeCanvasPointToMiddle : MonoBehaviour
{
    Vector3 centerScreen; 
    RectTransform rectTransform;

    RectTransform[] rectTransforms = null;
    List<RectTransform> fixedRectTransform = new List<RectTransform>();

    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [SerializeField] TextMeshProUGUI Targeting;

    FrogBrain frogBrain;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransforms = GetComponentsInChildren<RectTransform>();
        frogBrain = GetComponentInParent<FrogBrain>();
    }

    private void Start()
    {
        // Get the center position of the screen in world space
        //centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        centerScreen = Vector3.zero;
        //make a private list of the same obj that have an image
        foreach (RectTransform t in rectTransforms)
        {
            //use a try get component to make a specific transform in the array rotate
            if (t.TryGetComponent(out Image image) && t.name != "Background")
            {
                fixedRectTransform.Add(t);
            }
        }

        Targeting.text = frogBrain.frog.target.ToString();
    }

    private void Update()
    {
        // Get the direction from the UI element to the middle object
        Vector3 directionToMiddle = centerScreen - rectTransform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(directionToMiddle.z, directionToMiddle.x) * Mathf.Rad2Deg;

        // Apply the rotation to the UI element
        rectTransform.rotation = Quaternion.Euler(new Vector3(90f, -angle +45f, 0f));

        // Keep the text and the images of the UI static and not moving
        foreach (RectTransform t in fixedRectTransform)
        {
            if (t.name == "ArrowRight")
            {
                t.rotation = Quaternion.Euler(new Vector3(90f, 0f, 180f));
            }
            else
            {
                t.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            }
        }
    }

    public void LeftArrow()
    {
        int currentEnum = (int)frogBrain.frog.target - 1;
        Debug.Log(currentEnum);
        if (currentEnum < 0)
        {
            currentEnum = Enum.GetValues(typeof(LogicSO.Target)).Length - 1;
        }
        frogBrain.frog.target = (LogicSO.Target)currentEnum;

        Targeting.text = frogBrain.frog.target.ToString();
    }

    public void RightArrow()
    {
        int currentEnum = ((int)frogBrain.frog.target + 1) % Enum.GetValues(typeof(LogicSO.Target)).Length;
        frogBrain.frog.target = (LogicSO.Target)currentEnum;

        Targeting.text = frogBrain.frog.target.ToString();
    }
}
