using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(startMenu.gameObject);
            Destroy(finisGame.gameObject);
            Destroy(endGame.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            Destroy(gameObject);
            Destroy(backgroundMusic.gameObject);
            return;
        }


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(endGame);
        DontDestroyOnLoad(startMenu);
        DontDestroyOnLoad(finisGame);
        DontDestroyOnLoad(menu);
        DontDestroyOnLoad(hud);
        DontDestroyOnLoad(backgroundMusic);

        SaveToFile(records);
        player = FindObjectOfType<Player>();
        PlayerPrefs.DeleteAll();
        UploadDataToGame();
        UpdateRecords();

   
    }

    // Resources

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public Text[] recordTimes = new Text[15];

    // References

    public Player player;
    public FloatingTextManager floatingTextManager;
    public Weapon weapon;
    public EndGame endGame;
    public StartMenu startMenu;
    public finishGameScene finisGame;
    public Hud hud;
    public CharacterMenu menu;
    public MusicClass backgroundMusic;


    // logic

    public int pesos;
    public int experience;
    public int maxLevel;
    public int numberOfRecords = 0;
    public Record[] records = new Record[15];
    public int[] healtIncrease = new int[10];

    public bool playerIsChased = false;



    public void UploadDataToGame()
    {
        //Debug.Log(Path.Combine(Application.persistentDataPath, "/records.txt"));
        DataSaver data = LoadFromFile();

        for(int i=0; i<15; i++)
        {
            records[i].time = data.times[i];
            records[i].name = data.names[i];
        }
    }

    public void AddNewRecord(int time, string text)
    {
        Record temp = new Record();
        temp.time = time;
        temp.name = text;

        if (records[0].time > time || records[0].time == 0)
        {
            records[0] = temp;
        }
        else
        {
            return;
        }

        Array.Sort<Record>(records, (x, y) => x.time.CompareTo(y.time));

        for (int i = 0; i < 15; i++)
        {
            //Debug.Log(records[i].time);
        }

    }

    public void UpdateRecords()
    {
        int br = 0;
        string sec = "";
        string min = "";
        for (int i = 0; i < 15; i++)
        {

            if (records[i].time != 0)
            {

                if ((records[i].time / 1000) % 100 < 10)
                {
                    sec = "0" + ((records[i].time / 1000) % 100).ToString();
                }
                else
                {
                    sec = ((records[i].time / 1000) % 100).ToString();
                }

                if (records[i].time / 100000 < 10)
                {
                    min = "0" + (records[i].time / 100000).ToString();
                }
                else
                {
                    min = (records[i].time / 100000).ToString();
                }

                string txt = min + ":" + sec + "." + ((records[i].time % 1000) / 100).ToString();


                recordTimes[br++].text = txt + " " + records[i].name;
            }
        }

        for (int i = br; i < 15; i++)
        {
            recordTimes[i].text = "-------------------------";
        }
    }

    public void GameFinished()
    {
        finisGame.ShowFinishedScene();

        TimeSpan newRecord = startMenu.GetStopWatchTime();

        int time = newRecord.Milliseconds;
        time += newRecord.Seconds * 1000;
        time += newRecord.Minutes * 100000;

        AddNewRecord(time, startMenu.usernameText.text);
        UpdateRecords();

        numberOfRecords++;
        //Debug.Log("asddddddddddd");
        ResetPlayerInfo();
        SaveState();
        
        SaveToFile(records);

    }

    public void ResetPlayerInfo()
    {
        pesos = 0;
        experience = 0;
        weapon.weaponLevel = 0;
        player.maxHitpoint = 8;
        player.hitpoint = 8;
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }


    // Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //Debug.Log(weaponPrices.Count);
        //Debug.Log(weapon.weaponLevel + " To je level");
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;

    }

    // xp
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
            {
                return r;
            }


        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }


    // Save state

    public void SaveState()
    {
        string str = "";


        str += "0" + "|";
        str += pesos.ToString() + "|";
        str += experience.ToString() + "|";
        str += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", str);

        //Debug.Log(pesos + " Save");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.weaponLevel = int.Parse(data[3]);
        // records = 

        weapon.SetWeapon(weapon.weaponLevel);

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }


    public static void SaveToFile(Record[] record)
    {
        //string path = Path.Combine(Application.persistentDataPath, "/records.json");

        Debug.Log(Application.persistentDataPath);

        string path = "records.json";


        DataSaver data = new DataSaver(record);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        
    }

    public static DataSaver LoadFromFile()
    {
        string path = "records.json";

        if (File.Exists(path))
        {

            string json = File.ReadAllText(path);
            DataSaver data = JsonUtility.FromJson<DataSaver>(json);
            return data;
            
        }
        else
        {
            Debug.Log("Path is unreachable");
            return null;
        }
    }

}
