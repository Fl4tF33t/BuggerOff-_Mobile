using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[DefaultExecutionOrder(50)]
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] uiTexts;

    [SerializeField] private Button nextWaveButton;
    [SerializeField] private Button _fasterButton;
    private int bugBits;
    private int _currentTimeScale = 1;  

    private void Awake()
    {
        uiTexts = GetComponentsInChildren<TextMeshProUGUI>();
        //nextWaveButton = GetComponentInChildren<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //wave button logic
        nextWaveButton.interactable = true;
        nextWaveButton.onClick.AddListener(() => 
        { 
            StartCoroutine(WaveSystem.Instance.Waves());
            SetUI();
            nextWaveButton.interactable = false;
        });

        _fasterButton.onClick.AddListener(() => FasterTimeScale());
        WaveSystem.Instance.OnWaveCompleted += () => nextWaveButton.interactable = true;

        SetUI();

        GameManager.Instance.OnUIChange += () => SetUI();
    }

    private void FasterTimeScale()
    {
        _currentTimeScale++;
        if (_currentTimeScale > 2)
        {
            _currentTimeScale = 1;
        }
        Debug.Log("Current time scale is: " + _currentTimeScale);
        Time.timeScale = _currentTimeScale;
    }

    private void SetUI()
    {
        //UIUtextLogic
        uiTexts[0].text = GameManager.Instance.Health.ToString();
        bugBits = GameManager.Instance.BugBits;
        float newBugBits = bugBits;
        if (bugBits > 1000)
        {
            newBugBits = (float)bugBits / 1000f;
            newBugBits = (float)Math.Round(newBugBits, 1);
        }
        char k = bugBits > 1000 ? 'K' : ' ';
        uiTexts[1].text = newBugBits.ToString() + k.ToString();
        uiTexts[2].text = $"Wave: {WaveSystem.Instance.Wave + 1} /  {CSVReader.Instance.csvFile.Length}";
    }

}
