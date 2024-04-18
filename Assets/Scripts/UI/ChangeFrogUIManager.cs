using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFrogUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _changeFrogs;
    [SerializeField] private FrogSO[] _frogSOs;
    
    private GameObject _frogContainer;
    private Image[] _frogSprites;
    private GameObject _frogWheel;

    private GameObject[] _testing;
    // Start is called before the first frame update
    void Start()
    {
        GetFrogContainer();
        GetFrogWheel();


        GetFrogSprites();
        PopulateFrogSprites();

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
            _frogSprites[i].GetComponent<FrogSelectionUI>().SetFrogSO(_frogSOs[i]);
            _frogSprites[i].GetComponent<FrogSelectionUI>().SetFrogWheel(_frogWheel);
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