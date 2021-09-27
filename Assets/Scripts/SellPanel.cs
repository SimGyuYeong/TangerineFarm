using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Image sellIconImage = null;
    [SerializeField]
    private Image backgroundImage = null;
    [SerializeField]
    private Button purchaseButton = null;
    [SerializeField]
    private Text upgradeText = null;
    [SerializeField]
    private Sprite[] sellSprite = null;

    private Sell sell = null;
    private int sellNumber = 0;

    private void Start()
    {
        UpdateValues();
    }

    public void Init(int index)
    {
        sellNumber = index;
    }

    public void UpdateValues()
    {
        sell = GameManager.Instance.CurrentUser.sellList[sellNumber];
        sellIconImage.sprite = sellSprite[sell.sellNumber];
        priceText.text = string.Format("±Ö {0}°³ = {1}", sell.gyul, GameManager.Instance.UI.GetUnit((long)sell.getMoney));
        upgradeText.text = string.Format("°¡°Ý: {0}", GameManager.Instance.UI.GetUnit((long)sell.price));
        nameText.text = sell.sellName;
        amountText.text = string.Format("{0}", GameManager.Instance.UI.GetUnit(sell.amount));
        bool isActive = GameManager.Instance.CurrentUser.money >= sell.price ? true : false;    
        purchaseButton.gameObject.SetActive(isActive);
        purchaseButton.interactable = GameManager.Instance.CurrentUser.money >= sell.price;
        backgroundImage.color = GameManager.Instance.CurrentUser.gyul >= sell.gyul ? Color.white : Color.grey;
        nameText.color = GameManager.Instance.CurrentUser.gyul >= sell.gyul ? Color.white : Color.grey;
    }

    public void OnClickGyulSell()
    {
        if(GameManager.Instance.CurrentUser.gyul >= sell.gyul)
        {
            GameManager.Instance.CurrentUser.gyul -= sell.gyul;
            GameManager.Instance.CurrentUser.money += sell.getMoney;
            GameManager.Instance.UI.UpdatePropertyPanel();
            UpdateValues();
        }
    }

    public void OnClickUpgrade()
    {
        if(GameManager.Instance.CurrentUser.money >= sell.price)
        {
            GameManager.Instance.CurrentUser.money -= sell.price;
            GameManager.Instance.CurrentUser.sellList[sellNumber].amount++;
            GameManager.Instance.UI.UpdatePropertyPanel();
            UpdateValues();
        }
    }
}
