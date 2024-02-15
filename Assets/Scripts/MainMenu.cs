using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button load;
    public Button options;
    public Button quit;
    private void Awake()
    {
        Screen.SetResolution((Screen.height / 9 * 16), Screen.height, true);
        Debug.Log(Screen.height);
        Debug.Log((float)Screen.height / 1080f);

    }
    private void Start()
    {
        float scale = (float)Screen.height / 1080f;
        play.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        load.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        options.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        quit.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }

}
