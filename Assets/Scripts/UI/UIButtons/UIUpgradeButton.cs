using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUpgradeButton : UIButtonManager
{
    FrogBrain frogBrain;
    Image image;
    Sprite[] sprites;

    int level;

    private void Awake()
    {
        image = GetComponent<Image>();
        frogBrain = GetComponentInParent<FrogBrain>();
        sprites = frogBrain.frogSO.visualSO.userInterface.UIUpgradeButtons;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {

        switch (this.gameObject.name)
        {
            case "Damage":
                level = ++frogBrain.frogUpgrade.damageLevel;
                break;
            case "Attack Speed":
                level = ++frogBrain.frogUpgrade.attackSpeedLevel;
                break;
            case "Discipline":
                level = ++frogBrain.frogUpgrade.disciplineLevel;
                break;
            case "Range":
                level = ++frogBrain.frogUpgrade.rangeLevel;
                break;
        }

        if (level > 0 && level <= 5)
        {
            image.sprite = sprites[level];
            if(level == 5)
            {
                //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                image.raycastTarget = false;
            }
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
