using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingletone<GameManager>
{
    [SerializeField] private User user = null;
    public User CurrentUser { get { return user; } }

    [SerializeField]
    private UIManager uIManager = null;
    public UIManager UI {  get { return uIManager;  } }

    [SerializeField]
    private SoundManager soundManager = null;
    public SoundManager SOUND { get { return soundManager; } }
        
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
        soundManager = GetComponent<SoundManager>();
        LoadFromJson();
        uIManager.Init();
        uIManager.UpdatePropertyPanel();
        InvokeRepeating("GyulPerSecond", 0f, 1f);
        InvokeRepeating("AutoGyulChangeMoney", 0f, 1f);
        CurrentUser.gopValue = 1f;
    }

    public void OnClickTree()
    {
        SOUND.PlayEffectSound(0);
        CurrentUser.gyul += CurrentUser.mouseGpC;
        int chance = Random.Range(0, 100);
        if(UI.isPlenty == true)
        {
            gyulTextShow(2, CurrentUser.mouseGpC);
        }
        else if (chance < CurrentUser.doubleChance)
        {
            CurrentUser.gyul += CurrentUser.mouseGpC;
            gyulTextShow(2, CurrentUser.mouseGpC * 2);
        }
        else
        {
           gyulTextShow(1, CurrentUser.mouseGpC);
        }
        uIManager.UpdatePropertyPanel();
        UI.PlentyUp();
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
                CurrentUser.money += CurrentUser.TotalGcM;
                CurrentUser.gyul -= CurrentUser.baseGcM();
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
            user.sellList.Add(new Sell("�����Ǹ�", 0, 1));
            user.sellList.Add(new Sell("�����Ǹ�", 1, 100));
            user.sellList.Add(new Sell("�뷮�Ǹ�", 2, 750));
            user.upgradeList = new List<Upgrade>();
            user.upgradeList.Add(new Upgrade("���ֳ���", 0, "����")); //Ŭ���� �� ����
            user.upgradeList.Add(new Upgrade("����", 1, "����")); //Ŭ���� �� ����
            user.upgradeList.Add(new Upgrade("���", 2, "��ȭ")); //Ŭ���� �� ����
            user.upgradeList.Add(new Upgrade("�ϼ�", 3, "�ø���")); //�ʴ� �� ����
            user.upgradeList.Add(new Upgrade("��Ȯ���", 4, "��ȭ")); //�ʴ�  �� ����
            user.upgradeList.Add(new Upgrade("�Ǹž�ü", 5, "����")); //�ʴ� �� ��ȯ ����
            user.upgradeList.Add(new Upgrade("��ü ��", 6, "�ø���")); //�ʴ� �������� �� ���� ����
            user.eventList = new List<Event>();
            user.eventList.Add(new Event("Ȳ�ݱ� ���Ȯ��", 0, 5f));   
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