using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Image eventIconImage = null;
    [SerializeField]
    private Image backgroundImage = null;
    [SerializeField]
    private Button purchaseButton = null;
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
        
    }
}
