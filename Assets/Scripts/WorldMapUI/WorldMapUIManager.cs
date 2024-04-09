using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMapUIManager : MonoBehaviour
{
    [SerializeField] private Button _cancelPopUp;
    [SerializeField] private Button _london;
    [SerializeField] private Button _cairo;
    [SerializeField] private Button _kyoto;

    [SerializeField] private Image _popUp;
    [SerializeField] private WorldMapSO[] _citiesSO;

    private GameObject _citySelected = null;

    private void Start()
    {
        _cancelPopUp.onClick.AddListener(() => OnClosePopUp());
        _london.onClick.AddListener(() => OnClickedCity(_london));
        _cairo.onClick.AddListener(() => OnClickedCity(_cairo));
        _kyoto.onClick.AddListener(() => OnClickedCity(_kyoto));
    }

    private void OnClosePopUp()
    {
        _citySelected.gameObject.SetActive(false);
        _popUp.gameObject.SetActive(false);
    }

    private void OnClickedCity(Button city) {
        
        Debug.Log("City clicked: " + city.name);
        _citySelected = city.transform.parent.gameObject.transform.GetChild(0).gameObject;
        _citySelected.SetActive(true);
        int cityIndex = GetCityIndex(city);
        StartCoroutine(ShowPopUp(cityIndex));
    }

    IEnumerator ShowPopUp(int cityIndex)
    {
        TextMeshProUGUI title = _popUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        title.text = _citiesSO[cityIndex].GetCityTitle();
        yield return new WaitForSeconds(1f);
        _popUp.gameObject.SetActive(true);       
    }

    private int GetCityIndex(Button city)
    {
        int index = 0;
        switch (city.name)
        {
            case "London_Btn":
                index = 0;
                break;
            case "Cairo_Btn":
                index = 1;
                break;
            case "Kyoto_Btn":
                index = 2;
                break;
            default:
                Debug.Log("No city found!!!!");
                break;
        }
        return index;
    }
}
