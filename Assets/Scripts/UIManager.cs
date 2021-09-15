using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("¸Þ´º Äµ¹ö½º")]
    [SerializeField]
    private GameObject menu = null;
    [Header("±Ö º¸À¯·®")]
    [SerializeField]
    private Text gyulText = null;
    [Header("µ· º¸À¯·®")]
    [SerializeField]
    private Text moneyText = null;

    private void Start()
    {
        UpdateHaveAmountPanel();
    }

    public void UpdateHaveAmountPanel()
    {
        gyulText.text = string.Format("x {0}", GameManager.Instance.CurrentUser.gyul);
        moneyText.text = string.Format("x {0}", GameManager.Instance.CurrentUser.money);
    }

    public void OnClickTree()
    {
        GameManager.Instance.CurrentUser.gyul += GameManager.Instance.CurrentUser.gpc;
    }

    public void OnClickOpenMenu()
    {
        menu.gameObject.SetActive(true);
    }

    public void OnClickCloseMenu()
    {
        menu.gameObject.SetActive(false);
    }
}
