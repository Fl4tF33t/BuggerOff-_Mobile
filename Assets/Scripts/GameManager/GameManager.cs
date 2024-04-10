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

    public int Health { get { return health; } }
    public int BugBits { get {  return bugBits; } }

    private void Start()
    {
        base.Awake();
        bugBits = 196;
        waveButton.onClick.AddListener(() =>
        {
            StartCoroutine(WaveSystem.Instance.Waves());
            waveButton.gameObject.SetActive(false);
        });
        WaveSystem.Instance.OnWaveCompleted += () => { waveButton.gameObject.SetActive(true); };
        OnHealthChange = (amount) => { health += amount; };
        OnBugBitsChange = (amount) => { bugBits += amount; };

    }

}
