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

    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.json";

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        uIManager = GetComponent<UIManager>();
        LoadFromJson();
        InvokeRepeating("SaveToJson", 1f, 60f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
    }

    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    public void OnApplicationQuit()
    {
        SaveToJson();
    }
}
