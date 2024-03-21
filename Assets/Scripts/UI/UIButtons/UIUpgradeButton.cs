using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpgradeButton : UIButtonManager
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the upgrade, i assume opening anothger button to start the confirmation of upgrade. Need to establish if not enough money, can you still click the upgrade
        switch (this.gameObject.name)
        {
            case "Damage":
                //frogSO.upgradeSO.damage.increment = 
                break;
            case "Attack Speed":

                break;
            case "Discipline":

                break;
            case "Range":

                break;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //PlayerManager.Instance.isOnUI = true;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //PlayerManager.Instance.isOnUI = false;
        base.OnPointerExit(eventData);
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
