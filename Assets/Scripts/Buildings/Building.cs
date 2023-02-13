using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    public string buildingName;
    public int buildingWidth;//these are the sizes in tiles for the building
    public int buildingHeight;
    public Sprite sprite;
    public string buildingID;
    public string uniqueBuildingID;//this is an ID so we can reference a building of the same type. e.g. Building_Saw_Mill_2 vs Building_Saw_Mill_3

    public BuildingData buildingData;

    public virtual void Start()
    {
        Initialise();
    }

    public virtual void Initialise()
    {
        if(buildingData is BasicBuildingData)
        {
            Building b = gameObject.AddComponent<BasicBuilding>();
            b.buildingData = buildingData;
        }
        else if (buildingData is CrafterBuildingData)
        {
            Building b = gameObject.AddComponent<SimpleCrafter>();
            b.buildingData = buildingData;
        }
        else if (buildingData is InventoryBuildingData)
        {
            Building b = gameObject.AddComponent<InventoryBuilding>();
            b.buildingData = buildingData;
        }
        else if(buildingData is GeneratorBuildingData)
        {
            Building b = gameObject.AddComponent<Generator>();
            b.buildingData = buildingData;
        }
        Destroy(this);
    }

    public virtual void StartCreatingUI() //TO DO: Make the UI only appear after you do something, not just when it is created.
    {
        
    }

    public virtual void OpenBuildingUI()
    {
        
    }

    void UpdateData()
    {
        
    }
}
