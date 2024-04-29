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

    [SerializeField] private Button _changeFrogs;

    [SerializeField] private Image _popUpCityInfo;
    [SerializeField] private WorldMapSO[] _citiesSO;

    private GameObject _citySelected = null;
    private string _levelSelected ="";

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
        playerData = saving.PlayerData;

        _cancelButton.onClick.AddListener(() => OnClosePopUp());
        _londonButton.onClick.AddListener(() => OnClickedCity(_londonButton, _citiesSO[0]));
        _cairoButton.onClick.AddListener(() => OnClickedCity(_cairoButton, _citiesSO[1]));
        _kyotoButton.onClick.AddListener(() => OnClickedCity(_kyotoButton, _citiesSO[2]));

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
                        if(city.id == 2 && city.isCompleted)
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
                        if (city.id == 2 && city.isCompleted)
                        {
                            //_rioButton.transform.parent.gameObject.SetActive(true);
                        }
                    }
                }
                break;
        }

        //switch (saving.PlayerData.stars)
        //{
        //    case int when saving.PlayerData.stars < 4 && saving.PlayerData.level < 3 :
        //        _cairoButton.gameObject.SetActive(false);
        //        _kyotoButton.gameObject.SetActive(false);
        //        break;
        //    case int when saving.PlayerData.stars > 3 && saving.PlayerData.stars < 9 && saving.PlayerData.level < 5:
        //        _cairoButton.gameObject.SetActive(true);
        //        _kyotoButton.gameObject.SetActive(false);
        //        break;
        //    case int when saving.PlayerData.stars > 8 && saving.PlayerData.stars < 11 && saving.PlayerData.level == 5:
        //        _cairoButton.gameObject.SetActive(true);
        //        _kyotoButton.gameObject.SetActive(true);
        //        break;
        //    case int when saving.PlayerData.stars == 11 && saving.PlayerData.level == 6:
        //        _cairoButton.gameObject.SetActive(true);
        //        _kyotoButton.gameObject.SetActive(true);
        //        break;
        //}
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


        foreach (var item in playerData.cityList)
        {
            if (item.cityName.Contains(cityInfo.CityTitle))
            {
                Debug.Log("I have the city name");

                if (item.id == 1)
                {
                    Debug.Log("is completed");
                    SwitchCase(item.numberOfStars, _cityLevelOneButton);
                    _cityLevelTwoButton.interactable = item.isCompleted;
                }
                if (item.id == 2)
                {
                    SwitchCase(item.numberOfStars, _cityLevelTwoButton);
                }
            }
        }
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
        _citiesSelected = new GameObject[] {_cityLevelOneButton.transform.GetChild(0).gameObject, _cityLevelTwoButton.transform.GetChild(0).gameObject, _cityLevelThreeButton.transform.GetChild(0).gameObject};

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
