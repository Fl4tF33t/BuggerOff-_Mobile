using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrySelecting : MonoBehaviour
{
    public LayerMask layerMask;
    public LayerMask UI;
    public Canvas upgrade;

    public static bool onUI;

    private void Start()
    {
        InputManager.Instance.OnTouchTap += Instance_OnTouchTap;
    }

    private void Instance_OnTouchTap(object sender, System.EventArgs e)
    {
        Vector3 position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            upgrade.gameObject.SetActive(true);
        }
        else if(!onUI)
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
