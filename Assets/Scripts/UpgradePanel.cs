using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text upgradeText = null;
    [SerializeField]
    private Image upgradeIconImage = null;
    [SerializeField]
    private Image backgroundImage = null;
    [SerializeField]
    private Sprite[] upgradeSprite = null;

    private Upgrade upgrade = null;
    private int upgradeNumber = 0;

    public void Awake()
    {
        UpdateValues();
    }

    public void Init(int index)
    {
        upgradeNumber = index;
    }

    public void UpdateValues()
    {
        upgrade = GameManager.Instance.CurrentUser.upgradeList[upgradeNumber];
        upgradeIconImage.sprite = upgradeSprite[upgrade.upgradeNumber];
        priceText.text = string.Format("АЁАн: {0}", GameManager.Instance.UI.GetUnit(upgrade.price));
        nameText.text = upgrade.upgradeName;
        upgradeText.text = upgrade.buttonText;
        amountText.text = string.Format("{0}", GameManager.Instance.UI.GetUnit(upgrade.amount));
        backgroundImage.color = GameManager.Instance.CurrentUser.money >= upgrade.price ? Color.white : Color.grey;
        nameText.color = GameManager.Instance.CurrentUser.money >= upgrade.price ? Color.white : Color.grey;
    }

    public void OnClickUpgrade()
    { 
        if(GameManager.Instance.CurrentUser.money >= upgrade.price)
        {
            GameManager.Instance.CurrentUser.money -= upgrade.price;
            GameManager.Instance.CurrentUser.upgradeList[upgradeNumber].amount++;
            GameManager.Instance.UI.UpdatePropertyPanel();
            UpdateValues();
        }
    }
}
