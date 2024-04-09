using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.PackageManager;

public class WorldMapUIManager : MonoBehaviour
{
    [SerializeField] private Button _cancelPopUp;
    [SerializeField] private Button _london;
    [SerializeField] private Button _cairo;
    [SerializeField] private Button _kyoto;

    [SerializeField] private Image _popUpCityInfo;
    [SerializeField] private WorldMapSO[] _citiesSO;

    private GameObject _citySelected = null;

    private void Start()
    {
        _cancelPopUp.onClick.AddListener(() => OnClosePopUp());
        _london.onClick.AddListener(() => OnClickedCity(_london, _citiesSO[0]));
        _cairo.onClick.AddListener(() => OnClickedCity(_cairo, _citiesSO[1]));
        _kyoto.onClick.AddListener(() => OnClickedCity(_kyoto, _citiesSO[2]));
    }

    private void OnClosePopUp()
    {
        _citySelected.gameObject.SetActive(false);
        _popUpCityInfo.gameObject.SetActive(false);
    }

    private void OnClickedCity(Button city, WorldMapSO cityInfo  ) {
        
        Debug.Log("City clicked: " + city.name);
        _citySelected = city.transform.parent.gameObject.transform.GetChild(0).gameObject;
        _citySelected.SetActive(true);
        StartCoroutine(ShowCityInfo(cityInfo));
    }

    IEnumerator ShowCityInfo(WorldMapSO cityInfo)
    {
        GetCityTitle(cityInfo);        
        GetCityLevelImages(cityInfo);

        yield return new WaitForSeconds(1f);
        _popUpCityInfo.gameObject.SetActive(true);       
    }
    
    private void GetCityTitle(WorldMapSO cityInfo)
    {
        TextMeshProUGUI title = _popUpCityInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        title.text = cityInfo.GetCityTitle();
    }
    
    private void GetCityLevelImages(WorldMapSO cityInfo)
    {
        Sprite[] currentCityLevels = cityInfo.GetCityLevelSprites();

        Image[] cityLevelImages = { _popUpCityInfo.transform.GetChild(1).GetComponent<Image>(), _popUpCityInfo.transform.GetChild(2).GetComponent<Image>(), _popUpCityInfo.transform.GetChild(3).GetComponent<Image>() };

        for (int i = 0; i < currentCityLevels.Length; i++)
        {
            cityLevelImages[i].sprite = currentCityLevels[i];
        }        
    }
}
