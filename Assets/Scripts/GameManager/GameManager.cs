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

    public Action<int> OnHealthChange;
    public Action<int> OnBugBitsChange;

    protected override void Awake()
    {
        base.Awake();
        waveButton.onClick.AddListener(() =>
        {
            StartCoroutine(WaveSystem.Instance.Waves());
            waveButton.gameObject.SetActive(false);
        });
        WaveSystem.Instance.OnWaveCompleted += () => 
        { 
            waveButton.gameObject.SetActive(true); 
        };
    }

    private void Start()
    {
        OnHealthChange = (amount) => { health += amount; };
        OnBugBitsChange = (amount) => { bugBits += amount; };
    }

}
