using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Button waveButton;

    [SerializeField]
    private int health;
    [SerializeField]
    private int bugBits;

    public Action<int> HealthChange;
    public Action<int> BugBitsChange;

    public event Action OnUIChange;

    public int Health { get { return health; } }
    public int BugBits { get {  return bugBits; } }



    private void Start()
    {
        base.Awake();
        bugBits = 976;
        waveButton.onClick.AddListener(() =>
        {
            StartCoroutine(WaveSystem.Instance.Waves());
            waveButton.gameObject.SetActive(false);
        });
        WaveSystem.Instance.OnWaveCompleted += () => waveButton.gameObject.SetActive(true); 
        WaveSystem.Instance.OnLevelCompleted += () => SceneManager.LoadScene("WorldLevel"); 

        HealthChange = (amount) => { health += amount; OnUIChange?.Invoke();
            if (health <= 0)
            { SceneManager.LoadScene("WorldLevel"); }
        };
        BugBitsChange = (amount) => { bugBits += amount; OnUIChange?.Invoke(); };
    }

}
