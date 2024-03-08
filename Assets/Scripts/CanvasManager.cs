using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas;
    private void Awake()
    {
        //Screen.SetResolution((Screen.height / 9 * 16), Screen.height, true);
        Debug.Log(Screen.height);
        Debug.Log((float)Screen.height / 1080f);
        if (Screen.height < 1080)
        {
            float res = Screen.width / Screen.height;
            if (res == 16 / 9)
            {
                canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
            }
            else if (res < 16 / 9)
            {
                canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1920 / res);
            }
            else if (res > 16 / 9)
            {
                canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080 * res, 1080);
            }
        }
        else if (Screen.height >= 1080)
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        }
    }
}
