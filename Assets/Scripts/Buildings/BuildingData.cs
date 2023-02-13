using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BuildingData : ScriptableObject{

    public string ID;
    public string uniqueBuildingID;
    public string buildingName;
    public Sprite sprite;
    public Sprite unbuiltSprite;// Sprite of the building when it is still unbuilt
    public int buildingWidth = 2;
    public int buildingHeight = 2;

    public int health;

    public BuildingUILayout buildingUILayout;

    public ItemData[] resourceType; // this is the different types of resources eg index 0 could be wood, index 1 could be stone that are required to make the building.
    public int[] resorceAmount; // this is the amount of resources needed per resource type using the same index as the type.

    public abstract void Initialise(GameObject obj);

    /*[System.Serializable]
    public class CrafterBuildingData : BasicBuildingData
    {
        public ItemData[] ingredients; //this is the ingredients the building takes
        public ItemData[] craftables;// this is the ingredients the building can output
    }

    [System.Serializable]
    public class StorageBuildingData : BasicBuildingData
    {
        public ItemData[] itemTypes; // these are the items that are allowed in this building
    }*/
}
