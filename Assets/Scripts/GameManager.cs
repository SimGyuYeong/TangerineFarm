using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingletone<GameManager>
{
    [SerializeField]
    private User user = null;
    public User CurrentUser { get { return user; } }

    private UIManager uIManager = null;
    public UIManager UI {  get { return uIManager;  } }
        
    private string SAVE_DATA_PATH;
    private string SAVE_FILE_NAME = "/SaveFile.txt";

    [SerializeField]
    private Transform pool = null;
    public Transform Pool { get { return pool; } }

    [SerializeField]
    private GyulText gyulTextTemplate = null;

    public bool autoSell = true;

    private void Awake()
    {
        SAVE_DATA_PATH = Application.persistentDataPath;
        uIManager = GetComponent<UIManager>();
        LoadFromJson();
        uIManager.Init();
        uIManager.UpdatePropertyPanel();
        InvokeRepeating("GyulPerSecond", 0f, 1f);
        InvokeRepeating("AutoGyulChangeMoney", 0f, 1f);
        uIManager.AllMenuButtonFalse();
    }

    public void OnClickTree()
    {
        CurrentUser.gyul += CurrentUser.mouseGpC;
        int chance = Random.Range(0, 100);
        if (chance < CurrentUser.doubleChance)
        {
            CurrentUser.gyul += CurrentUser.mouseGpC;
            gyulTextShow(2, CurrentUser.mouseGpC * 2);
        }
        else
        {
           gyulTextShow(1, CurrentUser.mouseGpC);
        }
        uIManager.UpdatePropertyPanel();
    }

    private void gyulTextShow(int check, long addGyul)
    {
        GyulText gyulText = null;
        if (Pool.childCount > 0)
        {
            gyulText = Pool.GetChild(0).GetComponent<GyulText>();
            gyulText.transform.SetParent(gyulTextTemplate.transform.parent);
        }
        else
        {
            gyulText = Instantiate(gyulTextTemplate, gyulTextTemplate.transform.parent).GetComponent<GyulText>();
        }
        gyulText.Show(Input.mousePosition, check, addGyul);
    }

    private void GyulPerSecond()
    {
        CurrentUser.gyul += CurrentUser.gPs;
        uIManager.UpdatePropertyPanel();
    }

    private void AutoGyulChangeMoney()
    {
        if (autoSell)
        {
            if (CurrentUser.TotalGcM >= 1)
            {
                long amount = CurrentUser.baseGcM();
                CurrentUser.money += CurrentUser.TotalGcM;
                CurrentUser.gyul -= amount;
                uIManager.UpdatePropertyPanel();
            }
        }
    }

    private void LoadFromJson()
    {
        if (File.Exists(string.Concat(SAVE_DATA_PATH, SAVE_FILE_NAME)))
        {
            string jsonString = File.ReadAllText(string.Concat(SAVE_DATA_PATH, SAVE_FILE_NAME));
            user = JsonUtility.FromJson<User>(jsonString);
        }
        else
        {
            user = new User();
            user.gyul = 0;
            user.money = 0;
            user.sellList = new List<Sell>();
            user.sellList.Add(new Sell("낱개판매", 0, 1));
            user.sellList.Add(new Sell("상자판매", 1, 100));
            user.sellList.Add(new Sell("대량판매", 2, 750));
            user.upgradeList = new List<Upgrade>();
            user.upgradeList.Add(new Upgrade("감귤나무", 0, "성장")); //클릭당 귤 증가
            user.upgradeList.Add(new Upgrade("수질", 1, "개선")); //클릭당 귤 증가
            user.upgradeList.Add(new Upgrade("비료", 2, "강화")); //클릭당 귤 증가
            user.upgradeList.Add(new Upgrade("일손", 3, "늘리기")); //초당 귤 증가
            user.upgradeList.Add(new Upgrade("수확기계", 4, "강화")); //초당  귤 증가
            user.upgradeList.Add(new Upgrade("판매업체", 5, "섭외")); //초당 귤 교환 증가
            user.upgradeList.Add(new Upgrade("업체 수", 6, "늘리기")); //초당 가져가는 귤 개수 증가
            user.eventList = new List<Event>();
            user.eventList.Add(new Event("더블클릭", 0, 5f));   
            SaveToJson();
        }
    }

    private void SaveToJson()
    {
        string jsonString = JsonUtility.ToJson(CurrentUser, true);
        File.WriteAllText(string.Concat(SAVE_DATA_PATH, SAVE_FILE_NAME), jsonString, System.Text.Encoding.UTF8);
    }

    public void OnApplicationQuit()
    {
        SaveToJson();
    }
}