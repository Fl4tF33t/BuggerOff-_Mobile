using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerManager : Singleton<PlayerManager>
{
    public FrogBrain selectedFrog = null;

    //Variables for UI elements
    public Canvas canvas = null;
    LayerMask frogLayer;
    public bool isOnUI = false;

    private void Start()
    {
        frogLayer = LayerMask.GetMask("Frog");
        InputManager.Instance.OnTouchTap += InputManager_OnTouchTap;
    }

    private void InputManager_OnTouchTap(object sender, InputManager.OnTouchTapEventArgs e)
    {
        Ray ray = Camera.main.ScreenPointToRay(e.screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, frogLayer))
        {
            selectedFrog = hit.transform.GetComponent<FrogBrain>();
            selectedFrog.UpgradeUI(true);
            //canvas.transform.gameObject.SetActive(true);
        }
        else if (!isOnUI)
        {
            if(selectedFrog != null)
            {
                selectedFrog.UpgradeUI(false);
                selectedFrog = null;
            }
        }
    }
}
