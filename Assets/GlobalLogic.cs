using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLogic : MonoBehaviour
{
    [SerializeField]
    private UniversalRendererData universalRendererData;

    private void Start()
    {
        Debug.Log(universalRendererData.rendererFeatures);
        universalRendererData.rendererFeatures[0].SetActive(true);
    }

}
