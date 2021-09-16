using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using System;

public class UIManager : MonoBehaviour
{
    [Header("±Ö º¸À¯·®")]
    [SerializeField]
    private Text gyulText = null;
    [Header("µ· º¸À¯·®")]
    [SerializeField]
    private Text moneyText = null;
    [SerializeField]
    private RectTransform controlPanel = null;
    public GameObject[] scrollArr = null;
    public Image[] buttonArr = null;

    private bool isOn = false;
    private bool scrollOn = false;
    private int scrollNum;

    private void Start()
    {
        UpdatePropertyPanel();
    }

    public void UpdatePropertyPanel()
    {
        GetUnit(GameManager.Instance.CurrentUser.gyul);
        gyulText.text = string.Format("x {0}", GetUnit(GameManager.Instance.CurrentUser.gyul));
        moneyText.text = string.Format("x {0}", GetUnit(GameManager.Instance.CurrentUser.money));
    }

    private string GetUnit(long unit)
    {
        if(unit >= 1000)
        {
            int placeN = 3;
            BigInteger value = unit;
            List<int> numList = new List<int>();
            int p = (int)Mathf.Pow(10, placeN);
            do
            {
                numList.Add((int)(value % p));
                value /= p;
            }
            while (value >= 1);

            int num = numList.Count < 2 ? numList[0] : numList[numList.Count - 1] * p + numList[numList.Count - 2];
            double f = (num / (float)p);
            f = Math.Round(f, 3);
            return f.ToString() + " " + GetUnitText(numList.Count - 1);
        }
        else
        {
            return unit.ToString();
        }
        
    }

    private string GetUnitText(int index)
    {
        index--;
        if (index < 0) return "";
        int repeatCount = (index / 26) + 1;
        string retStr = "";
        for(int i = 0; i < repeatCount; i++) retStr += (char)(65 + index % 26);
        return retStr;
    }

    public void OnToggleChanged()
    {
        controlPanel.DOAnchorPosX(isOn ? 140f : -200f, 0.7f);
        isOn = !isOn ? isOn = true : isOn = false;
    }

    public void OnClickScrollButton(int num)
    {
        for (int i = 0; i < 3; i++)
        {
            scrollArr[i].SetActive(false);
            buttonArr[i].color = Color.white;
        }
        if(scrollNum != num)
        {
            scrollArr[scrollNum].SetActive(false);
            buttonArr[scrollNum].color = Color.white;
            scrollArr[num].SetActive(true);
            buttonArr[num].color = Color.green;
            scrollOn = true;
            scrollNum = num;
            return;
        }
        if (!scrollOn)
        {
            buttonArr[num].color = Color.green;
            scrollArr[num].SetActive(true);
            scrollOn = true;
        }
        else
        {
            buttonArr[num].color = Color.white;
            scrollOn = false;
        }
    }
}
