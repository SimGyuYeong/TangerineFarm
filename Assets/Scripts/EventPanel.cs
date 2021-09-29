using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [SerializeField] private Text nameText = null;
    [SerializeField] private Text chanceText = null;
    [SerializeField] private Text priceText = null;
    [SerializeField] private Image eventIconImage = null;
    [SerializeField] private Image backgroundImage = null;
    [SerializeField]
    private Sprite[] eventSprite = null;

    private Event eventp = null;
    private int eventNumber = 0;

    public void Awake()
    {
        UpdateValues();
    }

    public void Init(int index)
    {
        eventNumber = index;
    }

    public void UpdateValues()
    {
        eventp = GameManager.Instance.CurrentUser.eventList[eventNumber];
        eventIconImage.sprite = eventSprite[eventp.eventNumber];
        priceText.text = string.Format("АЁАн: {0}", GameManager.Instance.UI.GetUnit(eventp.price));
        nameText.text = eventp.eventName;
        chanceText.text = string.Format("{0}%", GameManager.Instance.UI.GetUnit((long)eventp.eventChance));
        backgroundImage.color = GameManager.Instance.CurrentUser.money >= eventp.price ? Color.white : Color.grey;
        nameText.color = GameManager.Instance.CurrentUser.money >= eventp.price ? Color.white : Color.grey;
    }

    public void OnClickUpgrade()
    {
        if (GameManager.Instance.CurrentUser.money >= eventp.price)
        {
            GameManager.Instance.CurrentUser.money -= eventp.price;
            GameManager.Instance.CurrentUser.eventList[eventNumber].eventChance++;
            GameManager.Instance.UI.UpdatePropertyPanel();
            UpdateValues();
        }
    }
}
