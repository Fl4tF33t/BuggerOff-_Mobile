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
                frogBrain.frog.damage += frogBrain.frogSO.upgradeSO.damage.amount;
                break;
            case "Attack Speed":
                level = ++frogBrain.frogUpgrade.attackSpeedLevel;
                frogBrain.frog.attackSpeed += frogBrain.frogSO.upgradeSO.attackSpeed.amount;
                break;
            case "Discipline":
                level = ++frogBrain.frogUpgrade.disciplineLevel;
                frogBrain.frog.discipline += frogBrain.frogSO.upgradeSO.discipline.amount;
                break;
            case "Range":
                level = ++frogBrain.frogUpgrade.rangeLevel;
                frogBrain.frog.range += frogBrain.frogSO.upgradeSO.range.amount;
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
