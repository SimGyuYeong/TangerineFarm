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
    private GyulImage gyulImageTemplate = null;

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
        uIManager.UpdatePropertyPanel();

        GyulImage gyulImage = null;
        if (Pool.childCount > 0)
        {
            gyulImage = Pool.GetChild(0).GetComponent<GyulImage>();
            gyulImage.transform.SetParent(gyulImageTemplate.transform.parent);
        }
        else
        {
            gyulImage = Instantiate(gyulImageTemplate, gyulImageTemplate.transform.parent).GetComponent<GyulImage>();
        }
        gyulImage.Show(Input.mousePosition);
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
