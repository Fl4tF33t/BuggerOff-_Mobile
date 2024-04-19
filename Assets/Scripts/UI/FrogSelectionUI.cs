using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class FrogSelectionUI : MonoBehaviour, IPointerDownHandler
{
    private FrogSO _frogSO;
    private GameObject _frogWheel;
    private GameObject _description;

    FrogChangeSelection _frogChangeSelection;

    private void Start()
    {
        _frogChangeSelection = transform.parent.parent.parent.GetComponent<FrogChangeSelection>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SetFrogTitle();
        SetFrogPrice();
        SetFrogStats();
        SetFrogDescription();
        _frogChangeSelection.OnFrogSelected(gameObject);
    }

    private void SetFrogDescription()
    {
        _description.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _frogSO.visualSO.userInterface.UIShopTextInfo;
    }

    private void SetFrogStats()
    {
        GameObject[] stats = new GameObject[_description.transform.GetChild(1).childCount];

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = _description.transform.GetChild(1).GetChild(i).gameObject;
        }

        stats[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.discipline.ToString();
        stats[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.damage.ToString();
        stats[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.range.ToString();
        stats[3].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.attackSpeed.ToString();
    }

    private void SetFrogPrice() 
    {
        _description.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.cost.ToString();
    }

    public void SetFrogSO(FrogSO frogSO)
    {
       _frogSO = frogSO;
    }

    public void SetFrogWheel(GameObject frogWheel)
    {
        _frogWheel = frogWheel;
        _description = frogWheel.transform.GetChild(0).GetChild(1).gameObject;
        //Debug.Log("2Name is: " + _frogWheel.name);
    }

    private void SetFrogTitle()
    {
        //Debug.Log("Name is: " + _frogWheel.name);
        _frogWheel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _frogSO.logicSO.frogName;
    }
}
