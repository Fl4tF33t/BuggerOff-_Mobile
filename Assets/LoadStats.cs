using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LoadStats : Singleton<LoadStats>
{
    public float bugHealth;
    public float bugSpeed;

    public float frogDamage;
    public float frogAttackSpeed;
    public float frogRange;
    public float jumpCoolDown;

    [SerializeField] private ScriptableRendererFeature fullScreenRenderer;

    private void Start()
    {
        if (fullScreenRenderer == null)
        {
            fullScreenRenderer = GetComponent<ScriptableRendererFeature>();
            Debug.Log("No full screen renderer found");
        }
        if(SceneManager.GetActiveScene().name.Contains("Sunny"))
        {
            bugHealth = 10f;
            bugSpeed = 0.1f;

            frogDamage = -5;
            frogAttackSpeed = -0.1f;
        }
         else if (SceneManager.GetActiveScene().name.Contains("Rainy"))
        {
            bugSpeed = -0.1f;

            frogDamage = 5f;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Foggy"))
        {
            bugSpeed = 0.1f;

            frogRange = -0.3f;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Snowy"))
        {
            bugHealth = -5;
            bugSpeed = -0.1f;

            frogAttackSpeed = -0.5f;
            fullScreenRenderer.SetActive(true);

        }
        else
        {
            fullScreenRenderer.SetActive(false);
        }
    }
}
