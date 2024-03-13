using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFrog : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask UI;
    public Canvas upgrade;

    private void Start()
    {
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
    }

    private void Instance_OnTouchTap(Vector2 obj)
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            upgrade.gameObject.SetActive(true);
        }
        else if (Physics.Raycast(ray, out hit, 100f, UI))
        {
            Debug.Log("ön ui");
        }
        else
        {
           upgrade.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        
    }

    private void Select()
    {
        
        
    }
}
