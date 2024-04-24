using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    private PlayerData playerData;

    private string path = "";
    private string persistentPath = "";


    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerData();
        SetPaths();
        DontDestroyOnLoad(this.gameObject);
    }

    private void CreatePlayerData()
    {
        int[] numberOfLevels = new int[6];
        for (int i = 0; i < numberOfLevels.Length; i++)
        {
            numberOfLevels[i] = 0;
        }
        playerData = new PlayerData(1,numberOfLevels, 0);
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

    public void SaveData()
    {
        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());
    }

    
}
