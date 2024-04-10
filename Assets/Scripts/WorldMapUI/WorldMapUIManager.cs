using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WorldMapUIManager : MonoBehaviour
{
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _londonButton;
    [SerializeField] private Button _cairoButton;
    [SerializeField] private Button _kyotoButton;
    [SerializeField] private Button _confirmButton;

    [SerializeField] private Image _popUpCityInfo;
    [SerializeField] private WorldMapSO[] _citiesSO;

    private GameObject _citySelected = null;
    private string _levelSelected ="";

    private Button _cityLevelOneButton;
    private Button _cityLevelTwoButton;
    private Button _cityLevelThreeButton;

    private GameObject[] _citiesSelected;



    private void Start()
    {
        _cancelButton.onClick.AddListener(() => OnClosePopUp());
        _londonButton.onClick.AddListener(() => OnClickedCity(_londonButton, _citiesSO[0]));
        _cairoButton.onClick.AddListener(() => OnClickedCity(_cairoButton, _citiesSO[1]));
        _kyotoButton.onClick.AddListener(() => OnClickedCity(_kyotoButton, _citiesSO[2]));

        _confirmButton.onClick.AddListener(() => OnConfirmButton());
    }

    private void OnConfirmButton()
    {
        if (_levelSelected == "")
        {
            Debug.Log("Level selected: " + _levelSelected);
        }
        else
        {
            //SceneManager.LoadScene(_levelSelected);
            SceneManager.LoadScene("CairoLvl1");
        }
    }

    private void OnClosePopUp()
    {
        UnselectCities();
        _levelSelected = "";
        DisableButtons(true);

        _citySelected.gameObject.SetActive(false);
        _popUpCityInfo.gameObject.SetActive(false);
    }

    private void OnClickedCity(Button city, WorldMapSO cityInfo  ) {
        DisableButtons(false);
        Debug.Log("City clicked: " + city.name);
        _citySelected = city.transform.parent.gameObject.transform.GetChild(0).gameObject;
        _citySelected.SetActive(true);
        StartCoroutine(ShowCityInfo(cityInfo));
    }

    IEnumerator ShowCityInfo(WorldMapSO cityInfo)
    {
        GetCityTitle(cityInfo);        
        GetCityLevelImages(cityInfo);
        GetCityLevelButtons(cityInfo);

        yield return new WaitForSeconds(1f);
        _popUpCityInfo.gameObject.SetActive(true);       
    }

    private void GetCityLevelButtons(WorldMapSO cityInfo)
    {
        _cityLevelOneButton = _popUpCityInfo.transform.GetChild(1).GetComponent<Button>();
        _cityLevelTwoButton = _popUpCityInfo.transform.GetChild(2).GetComponent<Button>();
        _cityLevelThreeButton = _popUpCityInfo.transform.GetChild(3).GetComponent<Button>();
        _citiesSelected = new GameObject[] {_cityLevelOneButton.transform.GetChild(0).gameObject, _cityLevelTwoButton.transform.GetChild(0).gameObject, _cityLevelThreeButton.transform.GetChild(0).gameObject};

        _cityLevelOneButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 1));
        _cityLevelTwoButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 2));
        _cityLevelThreeButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 3));
    }

    private void OnClickedLevel(WorldMapSO cityInfo, int levelSelected)
    {
        UnselectCities();

        _citiesSelected[levelSelected - 1].SetActive(true);
        _levelSelected = cityInfo.GetLevel(levelSelected);

        Debug.Log("Level selected: " + _levelSelected);
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

    private void UnselectCities()
    {
        foreach (GameObject city in _citiesSelected)
        {
            city.SetActive(false);
        }
    }

    private void DisableButtons(bool dissableButton)
    {
        _londonButton.interactable = dissableButton;
        _cairoButton.interactable = dissableButton;
        _kyotoButton.interactable = dissableButton;
    }
}
