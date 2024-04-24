using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public int level;
    public int[] starsEachLevel;
    public int stars;

    public PlayerData(int level, int[] starsEachlevel, int stars)
    {
        this.level = level;
        for (int i = 0; i < starsEachLevel.Length; i++)
        {
            this.starsEachLevel[i] = starsEachLevel[i];
        }
        this.stars = stars;
    }

    public override string ToString()
    {
        return $"The player is on level {level} and has {stars} all together.";
    }
}
