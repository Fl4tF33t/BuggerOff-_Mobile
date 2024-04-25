using System.IO;
using UnityEngine;

public class JSONSaving : Singleton<JSONSaving>
{
    [SerializeField]
    public PlayerData playerData;

    private string path = "";
    public string persistentPath = "";


    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        SetPaths();
        CreatePlayerData();
    }

    private void CreatePlayerData()
    {
        if (playerData.level == 0)
        {
            int[] numberOfLevels = new int[6];
            for (int i = 0; i < numberOfLevels.Length; i++)
            {
                numberOfLevels[i] = 0;
            }
            playerData = new PlayerData(1, numberOfLevels, 0);
            Debug.Log("My dick is big   " + playerData);
            SaveData(playerData);
        }
        
        //if (playerData != null)
        //{
        //    Debug.Log("there is a file already");
        //    //return;
        //}
        //else
        //{
        //    int[] numberOfLevels = new int[6];
        //    for (int i = 0; i < numberOfLevels.Length; i++)
        //    {
        //        numberOfLevels[i] = 0;
        //    }
        //    playerData = new PlayerData(1, numberOfLevels, 0);
        //    Debug.Log("My dick is big   " + playerData);
        //}   
    }
    
    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.jsno";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.jsno";
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData(PlayerData playerDataPlease)
    {
        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(playerDataPlease);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        playerData = JsonUtility.FromJson<PlayerData>(json);
        //Debug.Log(data.ToString());
    }

    public PlayerData ReturnPlayerData()
    {
        return playerData;
    }

    
}
