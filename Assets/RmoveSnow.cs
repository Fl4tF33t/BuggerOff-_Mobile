using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RmoveSnow : MonoBehaviour
{
    [SerializeField] private ScriptableRendererFeature fullScreenRenderer;
    private void Start()
    {
        if (fullScreenRenderer.isActive)
        {
            fullScreenRenderer.SetActive(false);
        }
        
    }

}
