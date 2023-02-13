using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStatistics
{

    //public int SceneID;
    //public int SkillPoints;

    public float PosX, PosY;

    //public Stat HP;
    //public Stat SP;
}

[Serializable]
public class SavedInventory
{
    [HideInInspector]
    public List<int> cellID;
    public List<string> item;
    public List<int> itemsInStack;

    public SavedInventory()
    {
        cellID = new List<int>();
        item = new List<string>();
        itemsInStack = new List<int>();
    }
}

[Serializable]
public class SavedInventoryList
{
    public List<SavedInventory> savedInventories;

    public SavedInventoryList()
    {
        savedInventories = new List<SavedInventory>();
    }
}

[Serializable]
public class SavedUnbuiltBuilding
{
    public float posX, posY;
    public string buildingID;

    public List<string> itemsNeededToCraft;
    public List<int> amountOfItemsNeeded;

    public SavedUnbuiltBuilding()
    {
        itemsNeededToCraft = new List<string>();
        amountOfItemsNeeded = new List<int>();
    }
}

[Serializable]
public class SavedBasicBuilding
{
    public float posX, posY;
    public string buildingID;
}

[Serializable]
public class SavedCrafterBuilding
{
    public string buildingID;
    public float posX, posY;

    public List<string> itemsInInventory;
    public List<int> itemStackAmount;

    public SavedCrafterBuilding()
    {
        itemsInInventory = new List<string>();
        itemStackAmount = new List<int>();
    }
}

[Serializable]
public class SavedInventoryBuilding
{
    public string buildingID;
    public float posX, posY;

    public List<string> itemsInInventory;
    public List<int> itemStackAmount;

    public SavedInventoryBuilding()
    {
        itemsInInventory = new List<string>();
        itemStackAmount = new List<int>();
    }
}

[Serializable]
public class SavedBuildingsList // this class is what stores all of the buildings in a list
{
    public List<SavedUnbuiltBuilding> savedUnbuiltBuildings; 
    public List<SavedBasicBuilding> savedBasicBuildings;
    public List<SavedCrafterBuilding> savedCrafterBuildings;
    public List<SavedInventoryBuilding> savedInventoryBuildings;

    public SavedBuildingsList()
    {
        savedUnbuiltBuildings = new List<SavedUnbuiltBuilding>();
        savedBasicBuildings = new List<SavedBasicBuilding>();
        savedCrafterBuildings = new List<SavedCrafterBuilding>();
        savedInventoryBuildings = new List<SavedInventoryBuilding>();
    }
}

[Serializable]
public class SavedWorldData
{
    public List<Vector3> resourceSeeds;
    //public int[] floorSeed;

    public SavedWorldData()
    {
        resourceSeeds = new List<Vector3>();
    }
}