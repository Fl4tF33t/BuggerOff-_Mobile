using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Canvas canvas;
    public Button play;
    public Button load;
    public Button options;
    public Button quit;
    [SerializeField] public AudioMixer _audioMixer;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MusicVolume") || !PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("BackgroundMusic", 1);
            PlayerPrefs.SetFloat("Sfx", 1);


        }

        _audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(PlayerPrefs.GetFloat("BackgroundMusic")) * 20);
        _audioMixer.SetFloat("Sfx", Mathf.Log10(PlayerPrefs.GetFloat("Sfx")) * 20);

        //Screen.SetResolution((Screen.height / 9 * 16), Screen.height, true);
        //Debug.Log(Screen.height);
        //Debug.Log((float)Screen.height / 1080f);
        //if (Screen.height < 1080 )
        //{
        //    float res = Screen.width / Screen.height;
        //    if (res==16/9)
        //    {
        //        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        //    }
        //    else if (res < 16/9)
        //    {
        //        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1920/res);
        //    }
        //    else if (res > 16/9)
        //    {
        //        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080*res, 1080);
        //    }
        //}
        //else if (Screen.height >= 1080)
        //{
        //    canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        //}
        //saving = transform.Find("JSONSave").GetComponent<JSONSaving>();
    }
    private void Start()
    {
        //float scale = (float)Screen.height / 1080f;
        //play.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        //load.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        //options.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        //quit.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

}
