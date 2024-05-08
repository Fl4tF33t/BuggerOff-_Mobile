using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WorldMapSO", menuName = "ScriptableObjects/WorldMapSO")]
public class WorldMapSO : ScriptableObject
{

    [SerializeField] private string _cityTitle;
    [SerializeField] public Sprite[] _levelSprites;
    [SerializeField] private string _LevelOne;
    [SerializeField] private string _LevelTwo;
    [SerializeField] private string _LevelThree;

    
    public string CityTitle
    {
        get { return _cityTitle; }
        private set { _cityTitle = value; }
    }

    public string GetCityTitle()
    {
        return _cityTitle;
    }

    public Sprite[] GetCityLevelSprites()
    {
        return _levelSprites;
    }

    public string GetLevel(int level)
    {
        switch (level)
        {
            case 1:
                return _LevelOne;
            case 2:
                return _LevelTwo;
            case 3:
                return _LevelThree;
            default:
                return "No level found";
        }
    }

    //private SOInfo infoSO;
    //public struct SOInfo 
    //{
    //    public string CityTitle;
    //    public Sprite[] LevelSprites;
    //}

    //public SOInfo InfoSO
    //{
    //    get { return infoSO; }
    //    private set 
    //    {

    //        infoSO = new SOInfo();

    //        infoSO.CityTitle = _cityTitle;
    //        infoSO.LevelSprites = _levelSprites;
    //        Debug.Log("Setting InfoSO");
    //        infoSO = value;
    //    }
    //}
}
