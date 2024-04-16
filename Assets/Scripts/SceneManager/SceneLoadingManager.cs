using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : Singleton<SceneLoadingManager>
{
    private const string WORLD_MAP_LEVEL = "WorldLevel";

    public void LoadSceneByInteger(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByString(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadWorldMapScene()
    {
        SceneManager.LoadScene(WORLD_MAP_LEVEL);
    }


}
