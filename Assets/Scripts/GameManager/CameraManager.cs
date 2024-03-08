using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer size;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        size = GameObject.Find("Size").GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        float screenRatio = (float)Screen.height / (float)Screen.width;
        float targetRatio = 9f / 16f;

        //if (screenRatio >= targetRatio)
        //{
        //    // Adjust orthographic size to fit target vertically
        //    cam.orthographicSize = size.bounds.size.y / 2 / targetRatio;
        //}
        //else
        //{
        //    // Adjust orthographic size to fit target horizontally
        //    cam.orthographicSize = size.bounds.size.y / 2;
        //}

        if(screenRatio <= targetRatio)
        {
            cam.orthographicSize = 5.2f;
        }
        else
        {
            //need to fix this
            cam.orthographicSize = 6.33f;
        }
        //else
        //{
        //    //make the bounds match the width first
        //}

        Debug.Log("Screen width is " + Screen.width);
        Debug.Log("Screen height is " + Screen.height);
        Debug.Log("Targetr ratio is " + targetRatio);

        //Debug.Log("Height is" + targetRatio);
    }
}
