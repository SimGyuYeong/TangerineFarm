using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("�޴� ĵ����")]
    [SerializeField]
    private GameObject menu = null;
    [Header("�� ������")]
    [SerializeField]
    private Text gyulText = null;
    [Header("�� ������")]
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
