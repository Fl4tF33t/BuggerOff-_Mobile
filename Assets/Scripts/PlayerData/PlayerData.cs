using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public List<City> cityList = new List<City>();

    private int numberOfLevels = 2;
    private string[] cityNames = new string[4] { "London", "Cairo", "Kyoto", "Rio" };

    public PlayerData()
    {
        foreach (var item in cityNames)
        {
            for (int i = 0; i < numberOfLevels; i++)
            {
                City city = new City();

            }
        }
    }

    [System.Serializable]
    public class City
    {
        public string cityName;
        public int id;
        public bool isCompleted;
        public int numberOfStars;
    }

    public override string ToString()
    {
        return $"The player is on level and has all together.";
    }
}
