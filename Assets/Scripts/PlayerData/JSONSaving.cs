using System;
using System.IO;
using UnityEngine;

public class JSONSaving : Singleton<JSONSaving>
{
    [SerializeField]
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get { return playerData; }
        set { playerData = value; SaveData(playerData); Debug.Log("Modify value"); }
    }

    private string persistentPath;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //create the path
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";

        if (File.Exists(persistentPath))
        {
            LoadData();
            Debug.Log("Load existing file");
        }
        else
        { 
            playerData = new PlayerData();
            PlayerData = playerData;
            Debug.Log("Majke new file");
        }

    }

    private void SaveData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(persistentPath, json);
    }

    private void LoadData()
    {
        string json = File.ReadAllText(persistentPath);
        playerData = JsonUtility.FromJson<PlayerData>(json);
    }
    
}
