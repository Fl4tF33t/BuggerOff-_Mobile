using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIUpgradeButton : UIButtonManager
{
    FrogBrain frogBrain;
    Image upgradeSprite;
    Sprite[] sprites;
    [SerializeField]
    Image[] confirm;


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
                switch (confirm[0].gameObject.activeSelf)
                {
                    case false:
                        confirm[0].gameObject.SetActive(true);
                        confirm[0].rectTransform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                        confirm[0].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = frogBrain.frogSO.logicSO.upgradeDamage.price.ToString();
                        confirm[0].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = frogBrain.frog.damage.ToString() + " --> " + (frogBrain.frogSO.logicSO.upgradeDamage.amount+ frogBrain.frog.damage).ToString();
                        break;
                    case true:
                        confirm[0].gameObject.SetActive(false);
                        if (frogBrain.frogUpgrade.damageLevel >= 0 && frogBrain.frogUpgrade.damageLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeDamage.price) >= 0)
                        {
                            GameManager.Instance.BugBitsChange?.Invoke(-frogBrain.frogSO.logicSO.upgradeDamage.price);
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
                }   
                break;
            case "Attack Speed":
                switch (confirm[1].gameObject.activeSelf)
                {
                    case false:
                        confirm[1].gameObject.SetActive(true);
                        confirm[1].rectTransform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                        confirm[1].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = frogBrain.frogSO.logicSO.upgradeAttackSpeed.price.ToString();
                        confirm[1].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = frogBrain.frog.attackSpeed.ToString() + " --> " + (frogBrain.frogSO.logicSO.upgradeAttackSpeed.amount+ frogBrain.frog.attackSpeed).ToString();
                        break;
                    case true:
                        confirm[1].gameObject.SetActive(false);
                        if (frogBrain.frogUpgrade.attackSpeedLevel >= 0 && frogBrain.frogUpgrade.attackSpeedLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeAttackSpeed.price) >= 0)
                        {
                            GameManager.Instance.BugBitsChange?.Invoke(-frogBrain.frogSO.logicSO.upgradeAttackSpeed.price);
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
                }
                break;
            case "Discipline":
                switch (confirm[3].gameObject.activeSelf)
                {
                    case false:
                        confirm[3].gameObject.SetActive(true);
                        confirm[3].rectTransform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                        confirm[3].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = frogBrain.frogSO.logicSO.upgradeDiscipline.price.ToString();
                        confirm[3].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = frogBrain.frog.discipline.ToString() + " --> " + (frogBrain.frogSO.logicSO.upgradeDiscipline.amount + frogBrain.frog.discipline).ToString();
                        break;
                    case true:
                        confirm[3].gameObject.SetActive(false);
                        if (frogBrain.frogUpgrade.disciplineLevel >= 0 && frogBrain.frogUpgrade.disciplineLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeDiscipline.price) >= 0)
                        {
                            GameManager.Instance.BugBitsChange?.Invoke(-frogBrain.frogSO.logicSO.upgradeDiscipline.price);
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
                }
                break;
            case "Range":
                switch (confirm[2].gameObject.activeSelf)
                {
                    case false:
                        confirm[2].gameObject.SetActive(true);
                        confirm[2].rectTransform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                        confirm[2].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = frogBrain.frogSO.logicSO.upgradeRange.price.ToString();
                        confirm[2].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = $"{frogBrain.frog.range} -->  {frogBrain.frogSO.logicSO.upgradeRange.amount+ frogBrain.frog.range}";
                        break;
                    case true:
                        confirm[2].gameObject.SetActive(false);
                        if (frogBrain.frogUpgrade.rangeLevel >= 0 && frogBrain.frogUpgrade.rangeLevel <= 5 && (GameManager.Instance.BugBits - frogBrain.frogSO.logicSO.upgradeRange.price) >= 0)
                        {
                            GameManager.Instance.BugBitsChange?.Invoke(-frogBrain.frogSO.logicSO.upgradeRange.price);
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
                break;
        }
    }
}
