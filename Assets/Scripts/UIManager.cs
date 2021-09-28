using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text gyulText = null;
    [SerializeField] private Text moneyText = null;
    [SerializeField] private Text ageText = null;
    [SerializeField] private Text gpsText = null;
    [SerializeField] private Text gcmText = null;
    [SerializeField] private RectTransform controlPanel = null;
    [SerializeField] private GameObject sellPanelTemplate = null;
    [SerializeField] private Transform sellContentRoot = null;
    [SerializeField] private GameObject upgradePanelTemplate = null;
    [SerializeField] private Transform upgradeContentRoot = null;
    [SerializeField] private GameObject eventPanelTemplate = null;
    [SerializeField] private Transform eventContentRoot = null;
    [SerializeField] private GameObject stopMenuPanel = null;
    [SerializeField] private Image autoSellButton = null;
    [SerializeField] private GameObject inputName = null;
    public GameObject helpCanvas = null;

    public GameObject[] scrollArr = null;
    public Image[] buttonArr = null;

    private List<SellPanel> sellPanelList = new List<SellPanel>();
    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();
    private List<EventPanel> eventPanelList = new List<EventPanel>();

    private bool isOn = false;
    private bool scrollOn = false;
    private int scrollNum;
    private bool isPause = false;

    public void Start()
    {
        if(GameManager.Instance.CurrentUser.name == "player")
        {
            inputName.SetActive(true);
            helpCanvas.SetActive(true);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Init()
    {
        GameObject newPanel = null;
        SellPanel sellPanelComponent = null;
        UpgradePanel upgradePanelComponent = null;
        EventPanel evenetPanelComponent = null;
        for (int i = 0; i < GameManager.Instance.CurrentUser.sellList.Count; i++)
        {
            newPanel = Instantiate(sellPanelTemplate, sellContentRoot);
            sellPanelComponent = newPanel.GetComponent<SellPanel>();
            sellPanelComponent.Init(i);
            sellPanelList.Add(sellPanelComponent);
            newPanel.SetActive(true);
        }

        for (int i = 0; i < GameManager.Instance.CurrentUser.upgradeList.Count; i++)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradeContentRoot);
            upgradePanelComponent = newPanel.GetComponent<UpgradePanel>();
            upgradePanelComponent.Init(i);
            upgradePanelList.Add(upgradePanelComponent);
            newPanel.SetActive(true);
        }

        for (int i = 0; i < GameManager.Instance.CurrentUser.eventList.Count; i++)
        {
            newPanel = Instantiate(eventPanelTemplate, eventContentRoot);
            evenetPanelComponent = newPanel.GetComponent<EventPanel>();
            evenetPanelComponent.Init(i);
            eventPanelList.Add(evenetPanelComponent);
            newPanel.SetActive(true);
        }
    }

    public void UpdatePropertyPanel()
    {
        gyulText.text = string.Format("x {0}", GetUnit(GameManager.Instance.CurrentUser.gyul));
        moneyText.text = string.Format("x {0}", GetUnit(GameManager.Instance.CurrentUser.money));
        ageText.text = string.Format("{0} {1}세", GameManager.Instance.CurrentUser.name, GetUnit(GameManager.Instance.CurrentUser.upgradeList[0].amount + 1));
        gpsText.text = string.Format("초당 귤: {0}개", GetUnit(GameManager.Instance.CurrentUser.gPs));
        gcmText.text = string.Format("초당 수익: {0}", GetUnit((GameManager.Instance.CurrentUser.upgradeList[6].amount+1) * GameManager.Instance.CurrentUser.upgradeList[5].amount));
        gcmText.color = !GameManager.Instance.autoSell ? Color.black : Color.white;
        RefreshList();
    }

    public void RefreshList()
    {
        for (int i = 0; i < sellPanelList.Count; i++)
        {
            sellPanelList[i].UpdateValues();
        }
        for (int i = 0; i < upgradePanelList.Count; i++)
        {
            upgradePanelList[i].UpdateValues();
        }
        for (int i = 0; i < eventPanelList.Count; i++)
        {
            eventPanelList[i].UpdateValues();
        }
    }

    public string GetUnit(long unit)
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
        controlPanel.DOAnchorPosX(isOn ? 262f : -410f, 0.8f);
        isOn = !isOn;
    }

    public void AllMenuButtonFalse()
    {
        for (int i = 0; i < 3; i++)
        {
            scrollArr[i].SetActive(false);
            buttonArr[i].color = Color.gray;
        }
        ageText.gameObject.SetActive(true);
        gcmText.gameObject.SetActive(true);
        gpsText.gameObject.SetActive(true);
    }

    public void OnClickScrollButton(int num)
    {
        AllMenuButtonFalse();
        if (scrollNum != num)
        {
            scrollArr[num].SetActive(true);
            buttonArr[num].color = Color.white;
            scrollOn = true;
            scrollNum = num;
            ageText.gameObject.SetActive(false);
            gcmText.gameObject.SetActive(false);
            gpsText.gameObject.SetActive(false);
            return;
        }

        if (!scrollOn)
        {
            buttonArr[num].color = Color.white;
            scrollArr[num].SetActive(true);
            scrollOn = true;
            ageText.gameObject.SetActive(false);
            gcmText.gameObject.SetActive(false);
            gpsText.gameObject.SetActive(false);
        }

        else
        {
            buttonArr[num].color = Color.gray;
            scrollArr[num].SetActive(false);
            scrollOn = false;
        }
    }

    public void OnClickAutoSellButton()
    {
        GameManager.Instance.autoSell = !GameManager.Instance.autoSell;
        UpdatePropertyPanel();
        autoSellButton.color = GameManager.Instance.autoSell ? Color.white : Color.gray;
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0f;
            stopMenuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            stopMenuPanel.SetActive(false);
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void OnEndEditEvent(string str)
    {
        GameManager.Instance.CurrentUser.name = str;
        inputName.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void Help()
    {
        helpCanvas.SetActive(true);
    }
}
