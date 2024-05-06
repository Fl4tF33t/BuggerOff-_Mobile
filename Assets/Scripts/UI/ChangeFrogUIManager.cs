using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFrogUIManager : MonoBehaviour
{
    private GameObject selectedFrog;
    public GameObject SelectedFrog{get; set;}

    [SerializeField] private GameObject _changeFrogs;
    [SerializeField] private FrogSO[] _frogSOs;
    
    private GameObject _frogContainer;
    private Image[] _frogSprites;
    private GameObject _frogWheel;

    private JSONSaving saving;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        saving = JSONSaving.Instance;
        if (saving != null)
        {
            playerData = saving.PlayerData;

        }

        GetFrogContainer();
        GetFrogWheel();


        GetFrogSprites();
        PopulateFrogSprites();

        UnlockFrogs();
        UnlockWheelSlots();
    }

    

    private void UnlockWheelSlots()
    {
        GameObject[] slots = new GameObject[8];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = _frogWheel.transform.GetChild(1).GetChild(i).gameObject;
        }
        for (int i = 3; i < slots.Length; i++)
        {
            if (playerData.cityList[i-3].isCompleted)
            {
                slots[i].transform.GetChild(1).gameObject.SetActive(true);
                slots[i].transform.GetChild(3).gameObject.SetActive(false);
                slots[i].transform.GetChild(1).GetComponent<Image>().sprite = _frogContainer.transform.GetChild(i).GetChild(0).GetChild(1).gameObject.GetComponent<Image>().sprite;
            }
        }
    }

    private void UnlockFrogs()
    {
        GameObject[] slots = new GameObject[10];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = _frogContainer.transform.GetChild(i).gameObject;
        }

        if (playerData.cityList[0].isCompleted)
        {
            slots[3].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[3].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            slots[3].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[3].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        if (playerData.cityList[1].isCompleted)
        {
            slots[4].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[4].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            slots[4].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[4].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        if (playerData.cityList[2].isCompleted)
        {
            slots[5].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[5].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            slots[5].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[5].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        if (playerData.cityList[3].isCompleted)
        {
            slots[6].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[6].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            slots[8].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[8].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            slots[6].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[6].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            slots[8].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[8].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        if (playerData.cityList[4].isCompleted)
        {
            slots[7].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[7].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            slots[9].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            slots[9].transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            slots[7].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[7].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            slots[9].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            slots[9].transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }


    }

    private void GetFrogWheel()
    {
        _frogWheel = _changeFrogs.transform.GetChild(2).gameObject;
    }

    private void GetFrogContainer()
    {
        _frogContainer = _changeFrogs.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    private void PopulateFrogSprites()
    {
        int i = 0;
        foreach (Image frogSprite in _frogSprites)
        {
            frogSprite.sprite = _frogSOs[i].visualSO.userInterface.UIShopSprite;
            i++;
        }
    }

    public void GetFrogSprites()
    {
        _frogSprites = new Image[_frogContainer.transform.childCount];

        Debug.Log("Amount of children " + _frogContainer.transform.childCount);

        for (int i = 0; i < _frogContainer.transform.childCount; i++)
        {
           _frogSprites[i] = _frogContainer.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Image>();

            _frogSprites[i].AddComponent<FrogSelectionUI>();
            FrogSelectionUI frogSelectionUI = _frogSprites[i].GetComponent<FrogSelectionUI>();

            frogSelectionUI.SetFrogSO(_frogSOs[i]);
            frogSelectionUI.SetFrogWheel(_frogWheel);

            //_frogSprites[i].GetComponent<FrogSelectionUI>().SetFrogSO(_frogSOs[i]);
            //_frogSprites[i].GetComponent<FrogSelectionUI>().SetFrogWheel(_frogWheel);
        }
    }

    public void CloseChangeFrogs()
    {
        _changeFrogs.SetActive(false);
    }

    public void OpenChangeFrogs()
    {
        _changeFrogs.SetActive(true);
    }
}
