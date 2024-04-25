using System;
using System.IO;
using UnityEngine;

public class JSONSaving : Singleton<JSONSaving>
{
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get { return playerData; }
        set { playerData = value; SaveData(playerData); }
    }

    private string path;
    public string persistentPath;

    protected override void Awake()
    {
        SetPaths();
        if (File.Exists(persistentPath))
        {
            LoadData();
        }
        else
        CreatePlayerData();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    private void CreatePlayerData()
    {
        int[] numberOfLevels = new int[6];
        for (int i = 0; i < numberOfLevels.Length; i++)
        {
            numberOfLevels[i] = 0;
        }
        playerData = new PlayerData(1, numberOfLevels, 0);

        PlayerData = playerData;
    }

    private void SaveData(PlayerData playerData)
    {
        string savePath = persistentPath;

        string json = JsonUtility.ToJson(playerData);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    private void LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        playerData = JsonUtility.FromJson<PlayerData>(json);
    }
    
}
