using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfWave : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnWaveCompleted += Instance_OnWaveCompleted;
    }

    private void Instance_OnWaveCompleted(object sender, System.EventArgs e)
    {
        int waveMoney = GameManager.Instance.wave * 10;
        GameManager.Instance.BugBitsChange(110 + waveMoney);
    }
}
