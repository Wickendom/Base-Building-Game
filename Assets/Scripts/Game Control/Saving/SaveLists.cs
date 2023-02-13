using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;

public class SaveLists : MonoBehaviour
{

    public static SaveLists Instance;

    private bool[] listFoundArray = new bool[3];

    //the below lists are used to have multiple lists inside them so i can control what list i use for each scene/ level
    /*public List<SavedItemList> savedObjectList = new List<SavedItemList>();
    public SavedUpgrades savedUpgrades;
    public List<SavedEnemyList> savedEnemyList = new List<SavedEnemyList>();*/
    //[HideInInspector]
    public SavedInventoryList savedInventoryList = new SavedInventoryList();
    [HideInInspector]
    public SavedBuildingsList savedBuildingList = new SavedBuildingsList();
    [HideInInspector]
    public SavedWorldData savedWorldData;

    public delegate void SaveDelegate(object sender, EventArgs args);
    public static event SaveDelegate SaveEvent;

    public delegate void LoadDelegate(object sender, EventArgs args);
    public static event LoadDelegate LoadEvent; 

    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    public PlayerStatistics localCopyOfData;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    void CheckLists()
    {
        if (savedInventoryList == null)
        {
            print("Saved Inventory list was null");
            savedInventoryList = new SavedInventoryList();
        }

        if(savedBuildingList == null)
        {
            print("Saved building list was null, creating list");
            savedBuildingList = new SavedBuildingsList();
        }

        /*if (savedObjectList == null)
        {
            print("Saved Object list was null");
            savedObjectList = new List<SavedItemList>();
        }

        if (savedEnemyList == null)
        {
            print("Saved Enemy List Was null");
            savedEnemyList = new List<SavedEnemyList>();
        }

        if (savedObsticalList == null)
        {
            print("Saved Obstical List Was null");
            savedObsticalList = new List<SavedObsticalList>();
        }*/
    }

    public SavedInventoryList GetInventoryLists()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
            {
                //print("got the scene list");
                return savedInventoryList;
            }
        }
        return null;
    }

    public SavedBuildingsList GetBuildingList()
    {
        return savedBuildingList;
    }

    /*public void ClearObsticalList()
    {
        for (int i = 0; i < savedObsticalList.Count; i++)
        {
            savedObsticalList[i].savedObsticals.Clear();
        }
    }

    public SavedObsticalList GetObsticalListForScene()
    {
        for (int i = 0; i < savedObsticalList.Count; i++)
        {
            if(savedObsticalList[i].SceneID == SceneManager.GetActiveScene().buildIndex)
            {
                return savedObsticalList[i];
            }
        }
        return null;
    }

   

    public SavedEnemyList GetEnemyListForScene()
    {
        for (int i = 0; i < savedEnemyList.Count; i++)
        {
            if (savedEnemyList[i].SceneID == SceneManager.GetActiveScene().buildIndex)
            {
               // print("got the enemy list");
                return savedEnemyList[i];
            }
        }
        return null;
    }

    public SavedUpgrades GetUpgradeList()
    {
        return savedUpgrades;
    }*/

    public void FireSaveEvent()
    {
        GetInventoryLists().savedInventories = new List<SavedInventory>();
        //GetEnemyListForScene().savedBosses = new List<SavedBoss>();
        
        //If we have any functions in the event:
        if (SaveEvent != null)
        {
            SaveEvent(null, null);
        }

    }

    public void FireLoadEvent()
    {
        if (LoadEvent != null)
        {
            LoadEvent(null, null);
        }
    }

    public void SaveData()
    {
        if (!Directory.Exists("Saves"))
        {
            Directory.CreateDirectory("Saves");
        }
        CheckLists();
        FireSaveEvent();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");
        FileStream saveInventories = File.Create("Saves/saveInventories.binary");
        FileStream saveBuildings = File.Create("Saves/saveBuildings.binary");

        localCopyOfData = PlayerState.Instance.localPlayerData;

        bf.Serialize(saveFile, localCopyOfData);
        bf.Serialize(saveInventories, savedInventoryList);
        bf.Serialize(saveBuildings, savedBuildingList);

        saveFile.Close();
        saveInventories.Close();
        saveBuildings.Close();

        print("saved");
    }

    public void LoadDataFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream loadFile = File.Open("Saves/save.binary", FileMode.Open);
        FileStream loadInventoriesFile = File.Open("Saves/saveInventories.binary", FileMode.Open);
        FileStream loadBuildingsFile = File.Open("Saves/saveBuildings.binary", FileMode.Open);
        //FileStream saveObjects = File.Open("Saves/saveObjects.binary", FileMode.Open);

        localCopyOfData = (PlayerStatistics)bf.Deserialize(loadFile);
        savedInventoryList = (SavedInventoryList)bf.Deserialize(loadInventoriesFile);
        savedBuildingList = (SavedBuildingsList)bf.Deserialize(loadBuildingsFile);
        //savedObjectList = (List<SavedItemList>)bf.Deserialize(saveObjects);

        loadFile.Close();
        loadInventoriesFile.Close();
        loadBuildingsFile.Close();
        //saveObjects.Close();

        FireLoadEvent();

        print("Loaded");
    }
}
