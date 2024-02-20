using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenu : MonoBehaviour
{
    public Canvas canvas;
    private void Awake()
    {
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
