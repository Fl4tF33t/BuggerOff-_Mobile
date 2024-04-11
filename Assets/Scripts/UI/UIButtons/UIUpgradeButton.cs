using System;
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
        frogBrain = GetComponentInParent<FrogBrain>();
        upgradeSprite = GetComponent<Image>();
        sprites = frogBrain.frogSO.visualSO.userInterface.UIUpgradeButtons;
    }
    private void Start()
    {
        if (this.gameObject.name=="Discipline")
        {
            frogBrain.frogUpgrade.disciplineLevel = frogBrain.frog.discipline;
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
            if (frogBrain.frogUpgrade.disciplineLevel == 5)
            {
                //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                upgradeSprite.raycastTarget = false;
            }
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        switch (this.gameObject.name)
        {
            case "Damage":
                if (frogBrain.frogUpgrade.damageLevel >= 0 && frogBrain.frogUpgrade.damageLevel <= 5 && (GameManager.Instance.BugBits-frogBrain.frogSO.logicSO.upgradeDamage.price)>=0)
                {
                    GameManager.Instance.BugBitsChange?.Invoke(frogBrain.frogSO.logicSO.upgradeDamage.price);
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
                    frogBrain.frog.damage += frogBrain.frogSO.logicSO.upgradeDamage.amount;
                    if (frogBrain.frogUpgrade.damageLevel == 5)
                    {
                        //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                        upgradeSprite.raycastTarget = false;
                    }
                }                
                break;
            case "Attack Speed":
                if (frogBrain.frogUpgrade.attackSpeedLevel >= 0 && frogBrain.frogUpgrade.attackSpeedLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeAttackSpeed.price) >= 0)
                {
                    GameManager.Instance.BugBitsChange?.Invoke(frogBrain.frogSO.logicSO.upgradeAttackSpeed.price);
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
                    frogBrain.frog.attackSpeed += frogBrain.frogSO.logicSO.upgradeAttackSpeed.amount;
                    if (frogBrain.frogUpgrade.attackSpeedLevel == 5)
                    {
                        //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                        upgradeSprite.raycastTarget = false;
                    }
                }
   
                break;
            case "Discipline":
                if (frogBrain.frogUpgrade.disciplineLevel >= 0 && frogBrain.frogUpgrade.disciplineLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeDiscipline.price) >= 0)
                {
                    GameManager.Instance.BugBitsChange?.Invoke(frogBrain.frogSO.logicSO.upgradeDiscipline.price);
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
                    frogBrain.frog.discipline += frogBrain.frogSO.logicSO.upgradeDiscipline.amount;
                    if (frogBrain.frogUpgrade.disciplineLevel == 5)
                    {
                        //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                        upgradeSprite.raycastTarget = false;
                    }
                }
                
                break;
            case "Range":
                if (frogBrain.frogUpgrade.rangeLevel >= 0 && frogBrain.frogUpgrade.rangeLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeRange.price) >= 0)
                {
                    GameManager.Instance.BugBitsChange?.Invoke(frogBrain.frogSO.logicSO.upgradeRange.price);
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
                    frogBrain.frog.range += frogBrain.frogSO.logicSO.upgradeRange.amount;
                    if (frogBrain.frogUpgrade.rangeLevel == 5)
                    {
                        //can also make the button itself non-interactable..if using button, dont need the onpointer click interface
                        upgradeSprite.raycastTarget = false;
                    }
                }                
                break;
        }
    }
}
