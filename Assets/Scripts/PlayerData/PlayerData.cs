using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public int[] starsEachLevel = new int[6];
    public int stars;

    public PlayerData(int level, int[] starsEachlevel, int stars)
    {
        this.level = level;
        this.starsEachLevel = starsEachlevel;
        this.stars = stars;
    }

    public override string ToString()
    {
        return $"The player is on level {level} and has {stars} all together.";
    }
}
