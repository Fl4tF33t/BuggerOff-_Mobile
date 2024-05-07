using System;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class FrogShopData : MonoBehaviour
{
    //Initializtion of the frog information
    private Image image;
    //public Action<FrogSO> OnSetFrogSO;

    private void Start()
    {
        image = GetComponent<Image>();

        //OnSetFrogSO = SetFrogSO;
    }
    public void SetFrogSO(FrogSO data)
    {
        image.sprite = data.visualSO.userInterface.UIShopSprite;
    }
}
