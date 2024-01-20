using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public delegate void TouchTapEvent();
    public event TouchTapEvent OnTouchTap;
    public delegate void TouchHoldEvent();
    public event TouchHoldEvent OnTouchHold;
    public delegate void TouchPressEvent();
    public event TouchPressEvent OnTouchPress;

    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions(); 
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        playerInputActions.Player.TouchTap.performed += TouchTap_performed;
        //playerInputActions.Player.TouchHold.performed += TouchHold_performed;
        //playerInputActions.Player.TouchInput.performed += TouchInput_performed;
        playerInputActions.Player.TouchPress.started += TouchPress_started;
        playerInputActions.Player.TouchPress.performed += TouchPress_performed;
        playerInputActions.Player.TouchPress.canceled += TouchPress_canceled;
    }

    private void TouchInput_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Touch input performed");
    }

    private void TouchPress_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Press " + obj.phase);
    }

    private void TouchPress_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Press " + obj.phase);
    }

    private void TouchPress_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Press " + obj.phase);
    }

    private void TouchHold_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Hold " + obj.phase);
    }

    private void TouchTap_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Tap " + obj.phase);
    }
}
