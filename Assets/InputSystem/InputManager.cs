using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputActions playerInputActions; 

    private void Awake()
    {
        playerInputActions = new PlayerInputActions(); 
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        playerInputActions.Player.TouchTap.performed += TouchTap_performed;
    }

    private void TouchTap_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("sdf");
    }
}
