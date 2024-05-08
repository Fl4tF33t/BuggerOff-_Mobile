using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


public class WorldMapUIManager : MonoBehaviour
{
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _londonButton;
    [SerializeField] private Button _cairoButton;
    [SerializeField] private Button _kyotoButton;
    [SerializeField] private Button _rioButton;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private GameObject _weatherScreen;
    [SerializeField] private Image _currentWeather;
    [SerializeField] private Button _changeFrogs;

    [SerializeField] private Image _popUpCityInfo;
    [SerializeField] private WorldMapSO[] _citiesSO;

    private GameObject _citySelected = null;
    private string _levelSelected = "";

    private Button _cityLevelOneButton;
    private Button _cityLevelTwoButton;
    private Button _cityLevelThreeButton;

    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite starGolden;

    private GameObject[] _citiesSelected;

    private JSONSaving saving;
    private PlayerData playerData;
    private int numberOfTotalStars;
    //private PlayerData playerData;

    private void Start()
    {
        saving = JSONSaving.Instance;
        if (saving != null)
        {
            playerData = saving.PlayerData;

        }

        _cancelButton.onClick.AddListener(() => OnClosePopUp());
        _londonButton.onClick.AddListener(() => OnClickedCity(_londonButton, _citiesSO[0]));
        _cairoButton.onClick.AddListener(() => OnClickedCity(_cairoButton, _citiesSO[1]));
        _kyotoButton.onClick.AddListener(() => OnClickedCity(_kyotoButton, _citiesSO[2]));
        _rioButton.onClick.AddListener(() => OnClickedCity(_rioButton, _citiesSO[3]));


        _confirmButton.onClick.AddListener(() => OnConfirmButton());

        

        //open the next maps
        for (int i = 0; i < playerData.cityList.Count; i++)
        {
            if (playerData.cityList[i].isCompleted)
            {
                numberOfTotalStars += playerData.cityList[i].numberOfStars;
            }
        }

        Debug.Log("num of stars" + numberOfTotalStars);

        switch (numberOfTotalStars)
        {
            case int when numberOfTotalStars > 3 && numberOfTotalStars <= 6:
                foreach (var city in playerData.cityList)
                {
                    if (city.cityName.Contains("London"))
                    {
                        if (city.id == 2 && city.isCompleted)
                        {
                            _cairoButton.transform.parent.gameObject.SetActive(true);
                        }
                    }
                }
                break;
            case int when numberOfTotalStars > 8 && numberOfTotalStars <= 12:
                _cairoButton.transform.parent.gameObject.SetActive(true);
                foreach (var city in playerData.cityList)
                {
                    if (city.cityName.Contains("Cairo"))
                    {
                        if (city.id == 2 && city.isCompleted)
                        {
                            _kyotoButton.transform.parent.gameObject.SetActive(true);
                        }
                    }
                }
                break;
            case int when numberOfTotalStars > 10 && numberOfTotalStars <= 15:
                _cairoButton.transform.parent.gameObject.SetActive(true);
                _kyotoButton.transform.parent.gameObject.SetActive(true);

                foreach (var city in playerData.cityList)
                {
                    if (city.cityName.Contains("Kyoto"))
                    {
                        if (city.id == 1 && city.isCompleted)
                        {
                            _rioButton.transform.parent.gameObject.SetActive(true);
                        }
                    }
                }
                break;
        }
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
            //SceneManager.LoadScene("CairoLvl1");

            //_levelSelected = "CairoLvl1";


            SceneLoadingManager.Instance.LoadSceneByString(_levelSelected);
        }
    }

    private void OnClosePopUp()
    {
        UnSelectCities();
        _levelSelected = "";
        DisableButtons(true);

        _citySelected.gameObject.SetActive(false);
        _popUpCityInfo.gameObject.SetActive(false);
    }

    private void OnClickedCity(Button city, WorldMapSO cityInfo)
    {
        if (Time.timeScale == 0)
        {
            Debug.Log("Time scale is 0");
            Time.timeScale = 1;
        }
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

        _cityLevelOneButton.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SetWeatherPanel(_cityLevelOneButton,cityInfo));
        _cityLevelTwoButton.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SetWeatherPanel(_cityLevelTwoButton,cityInfo));
        _weatherScreen.transform.GetChild(8).GetComponent<Button>().onClick.AddListener(() => _weatherScreen.SetActive(false));

        yield return new WaitForSeconds(1f);
        _popUpCityInfo.gameObject.SetActive(true);

        
        foreach (var item in playerData.cityList)
        {
            if (item.cityName.Contains(cityInfo.CityTitle))
            {
                Debug.Log("I have the city name");

                if (cityInfo.CityTitle == "Kyoto" || cityInfo.CityTitle == "Rio")
                {
                    _cityLevelTwoButton.interactable = false;
                    _cityLevelTwoButton.transform.GetChild(1).gameObject.SetActive(false);
                    _cityLevelTwoButton.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    _cityLevelTwoButton.transform.GetChild(1).gameObject.SetActive(true);
                    _cityLevelTwoButton.transform.GetChild(2).gameObject.SetActive(true);
                    if (item.id == 1)
                    {
                        Debug.Log("is completed");
                        SwitchCase(item.numberOfStars, _cityLevelOneButton);
                        _cityLevelTwoButton.interactable = item.isCompleted;
                        _cityLevelOneButton.transform.GetChild(2).GetComponent<Button>().interactable = item.isCompleted;
                    }
                    if (item.id == 2)
                    {
                        Debug.Log("Vau");
                        SwitchCase(item.numberOfStars, _cityLevelTwoButton);
                        _cityLevelTwoButton.transform.GetChild(2).GetComponent<Button>().interactable = item.isCompleted;
                    }
                }             
            }

            //if (item.cityName.Contains(cityInfo.CityTitle) || item.cityName.Contains(cityInfo.CityTitle))
            //{
            //    Debug.Log("heo");
            //    if (item.id == 2)
            //    {
                    
            //    }
            //}
            //else
            //{
            //    Debug.Log("vau");
            //    if (item.id == 2)
            //    {
            //        _cityLevelTwoButton.interactable = true;
            //        _cityLevelTwoButton.transform.GetChild(1).gameObject.SetActive(true);
            //        _cityLevelTwoButton.transform.GetChild(2).gameObject.SetActive(true);
            //    }
            //}
        }
    }

    private void SetWeatherPanel( Button btn, WorldMapSO cityInfo)
    {
        _weatherScreen.SetActive(true);
        string currentCity = "";
        string currentWeather = "_Sunny";
        if (btn == _cityLevelOneButton)
        {
            _weatherScreen.transform.GetChild(1).GetComponent<Image>().sprite = cityInfo._levelSprites[0];
            currentCity = cityInfo.CityTitle + "1";
        }
        else if (btn == _cityLevelTwoButton)
        {
            _weatherScreen.transform.GetChild(1).GetComponent<Image>().sprite = cityInfo._levelSprites[1];
            currentCity = cityInfo.CityTitle + "2";
        }
        _weatherScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => _currentWeather.sprite = _weatherScreen.transform.GetChild(3).GetComponent<Image>().sprite);
        _weatherScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => currentWeather = "_Sunny");

        
        _weatherScreen.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => _currentWeather.sprite = _weatherScreen.transform.GetChild(4).GetComponent<Image>().sprite);
        _weatherScreen.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => currentWeather = "_Foggy");

      
        _weatherScreen.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => _currentWeather.sprite = _weatherScreen.transform.GetChild(5).GetComponent<Image>().sprite);
        _weatherScreen.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => currentWeather = "_Snowy");


        _weatherScreen.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => _currentWeather.sprite = _weatherScreen.transform.GetChild(6).GetComponent<Image>().sprite);
        _weatherScreen.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => currentWeather = "_Rainy");
       

        _weatherScreen.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(currentCity + currentWeather));
    }

    private void SwitchCase(int num, Button btn)
    {
        switch (num)
        {
            case 0:
                btn.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = emptyStar;
                btn.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = emptyStar;
                btn.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = emptyStar;
                break;
            case 1:
                btn.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = starGolden;
                btn.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = emptyStar;
                btn.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = emptyStar;
                break;
            case 2:
                btn.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = starGolden;
                btn.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = starGolden;
                btn.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = emptyStar;
                break;
            case 3:
                btn.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = starGolden;
                btn.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = starGolden;
                btn.transform.GetChild(1).GetChild(2).GetComponent<Image>().sprite = starGolden;
                break;
        }
    }

    private void GetCityLevelButtons(WorldMapSO cityInfo)
    {
        _cityLevelOneButton = _popUpCityInfo.transform.GetChild(1).GetComponent<Button>();
        _cityLevelTwoButton = _popUpCityInfo.transform.GetChild(2).GetComponent<Button>();
        _cityLevelThreeButton = _popUpCityInfo.transform.GetChild(3).GetComponent<Button>();
        _citiesSelected = new GameObject[] { _cityLevelOneButton.transform.GetChild(0).gameObject, _cityLevelTwoButton.transform.GetChild(0).gameObject, _cityLevelThreeButton.transform.GetChild(0).gameObject };

        _cityLevelOneButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 1));
        _cityLevelTwoButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 2));
        _cityLevelThreeButton.onClick.AddListener(() => OnClickedLevel(cityInfo, 3));
    }

    private void OnClickedLevel(WorldMapSO cityInfo, int levelSelected)
    {
        UnSelectCities();

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

    private void UnSelectCities()
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
        _changeFrogs.interactable = dissableButton;
    }

    public void AddStar()
    {

    }
}
