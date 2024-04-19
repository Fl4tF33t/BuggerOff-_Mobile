using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetInitialDescription : MonoBehaviour
{
    [SerializeField] private FrogSO _commonFrogSO;

    private void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.frogName;
        transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.cost.ToString();
        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.discipline.ToString();
        transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.damage.ToString();
        transform.GetChild(1).GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.range.ToString();
        transform.GetChild(1).GetChild(1).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.logicSO.attackSpeed.ToString();
        transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = _commonFrogSO.visualSO.userInterface.UIShopTextInfo;
    }
}
