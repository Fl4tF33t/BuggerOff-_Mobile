using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUpgradeButton : UIButtonManager
{
    FrogBrain frogBrain;
    Image upgradeSprite;
    Sprite[] sprites;

    private void Awake()
    {
        upgradeSprite = GetComponent<Image>();
        frogBrain = GetComponentInParent<FrogBrain>();
        sprites = frogBrain.frogSO.visualSO.userInterface.UIUpgradeButtons;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //what to do when the button is clicked on the upgrade, i assume opening anothger button to start the confirmation of upgrade. Need to establish if not enough money, can you still click the upgrade
        switch (this.gameObject.name)
        {
            case "Damage":
                frogBrain.frogUpgrade.damageLevel++;
                switch (frogBrain.frogUpgrade.damageLevel)
                {
                    case 0:
                        break;
                    case 1:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.damageLevel];
                        break;
                    case 2:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.damageLevel];
                        break;
                    case 3:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.damageLevel];
                        break;
                    case 4:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.damageLevel];
                        break;
                    case 5:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.damageLevel];
                        break;
                }
                break;
            case "Attack Speed":
                frogBrain.frogUpgrade.attackSpeedLevel++;
                switch (frogBrain.frogUpgrade.attackSpeedLevel)
                {
                    case 1:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.attackSpeedLevel];
                        break;
                    case 2:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.attackSpeedLevel];
                        break;
                    case 3:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.attackSpeedLevel];
                        break;
                    case 4:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.attackSpeedLevel];
                        break;
                    case 5:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.attackSpeedLevel];
                        break;
                }
                break;
            case "Discipline":
                frogBrain.frogUpgrade.disciplineLevel++;
                switch (frogBrain.frogUpgrade.disciplineLevel)
                {
                    case 1:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.disciplineLevel];
                        break;
                    case 2:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.disciplineLevel];
                        break;
                    case 3:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.disciplineLevel];
                        break;
                    case 4:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.disciplineLevel];
                        break;
                    case 5:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.disciplineLevel];
                        break;
                }
                break;
            case "Range":
                frogBrain.frogUpgrade.rangeLevel++;
                switch (frogBrain.frogUpgrade.rangeLevel)
                {
                    case 1:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.rangeLevel];
                        break;
                    case 2:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.rangeLevel];
                        break;
                    case 3:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.rangeLevel];
                        break;
                    case 4:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.rangeLevel];
                        break;
                    case 5:
                        upgradeSprite.sprite = sprites[frogBrain.frogUpgrade.rangeLevel];
                        break;
                }
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
