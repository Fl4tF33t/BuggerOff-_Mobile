using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WorldMapSO", menuName = "ScriptableObjects/WorldMapSO")]
public class WorldMapSO : ScriptableObject
{

    [SerializeField] private string _cityTitle;
    [SerializeField] private Sprite[] _levelSprites;

    public string GetCityTitle()
    {
        return _cityTitle;
    }

    public Sprite[] GetCityLevelSprites()
    {
        return _levelSprites;
    }

 

}
