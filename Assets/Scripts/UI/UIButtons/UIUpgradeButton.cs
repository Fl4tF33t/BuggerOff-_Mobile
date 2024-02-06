using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpgradeButton : UIButtonManager
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the upgrade, i assume opening anothger button to start the confirmation of upgrade. Need to establish if not enough money, can you still click the upgrade
        Debug.Log("don't close please you vitun runkkari");
    }

    protected override void ActivatedHoldDown() 
    {
        Debug.Log("show Info");
    }
    protected override void DeactivatedHoldDown() 
    {
        Debug.Log("Hide the body");
    }
}
