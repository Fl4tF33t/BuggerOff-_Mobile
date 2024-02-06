using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectFrog : MonoBehaviour
{
    public LayerMask layerMask;
    public Canvas upgrade;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Select();
        }
    }

    private void Select()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, hit, 100f, layerMask)
        {

            upgrade.gameObject.SetActive(true);
        }
        //else
        //{

        //    upgrade.gameObject.SetActive(false);
        //}
    }
}
