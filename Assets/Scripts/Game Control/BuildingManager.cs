using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance;

    public List<BuildingData> fullBuildingList;// list of all buildings in the game. Not the buildings in the players game world.

    private UniqueBuildingIDHolder uniqueBuildingIDHolder;

    [SerializeField]
    private GameObject unbuitBuildingResourceUIGO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uniqueBuildingIDHolder = new UniqueBuildingIDHolder();
        uniqueBuildingIDHolder.Initialise();
    }

    private void OnEnable()
    {
        SaveLists.LoadEvent += LoadBuildings;
    }

    private void OnDisable()
    {
        SaveLists.LoadEvent -= LoadBuildings;
    }

    public void AddBuildingSlot()
    {
        fullBuildingList.Add(null);
    }

    public void RemoveBuilding(int index)
    {
        fullBuildingList.RemoveAt(index);
    }

    public BuildingData FindBuildingByID(string buildingID)// finds the standard building ID in the list of all buildings
    {
        for (int i = 0; i < fullBuildingList.Count; i++)
        {
            if (buildingID == fullBuildingList[i].ID)
            {
                return fullBuildingList[i];
            }
        }
        return null;
    }

    public string AddNewUniqueID(BasicBuilding basicBuilding)
    {
        uniqueBuildingIDHolder.listOfBasicBuildingsUniqueIDs.Add(basicBuilding.buildingID + "_" + uniqueBuildingIDHolder.listOfBasicBuildingsUniqueIDs.Count, basicBuilding);
        return basicBuilding.buildingID + "_" + (uniqueBuildingIDHolder.listOfBasicBuildingsUniqueIDs.Count - 1);
    }
    public string AddNewUniqueID(SimpleCrafter simpleCrafter)
    {
        uniqueBuildingIDHolder.listOfSimpleCrafterUniqueIDs.Add(simpleCrafter.buildingID+"_"+uniqueBuildingIDHolder.listOfSimpleCrafterUniqueIDs.Count,simpleCrafter);
        return simpleCrafter.buildingID + "_" + (uniqueBuildingIDHolder.listOfSimpleCrafterUniqueIDs.Count - 1);
    }
    public string AddNewUniqueID(Generator generator)
    {
        uniqueBuildingIDHolder.listOfGeneratorUniqueIDs.Add(generator.buildingID + "_" + uniqueBuildingIDHolder.listOfGeneratorUniqueIDs.Count, generator);
        return generator.buildingID + "_" + (uniqueBuildingIDHolder.listOfGeneratorUniqueIDs.Count - 1);
    }
    public string AddNewUniqueID(InventoryBuilding inventoryBuilding)
    {
        uniqueBuildingIDHolder.listOfInventoryBuildingsUniqueIDs.Add(inventoryBuilding.buildingID + "_" + uniqueBuildingIDHolder.listOfInventoryBuildingsUniqueIDs.Count, inventoryBuilding);
        return inventoryBuilding.buildingID + "_" + (uniqueBuildingIDHolder.listOfInventoryBuildingsUniqueIDs.Count - 1);
    }
    public string AddNewUniqueID(UnbuiltBuilding unbuiltBuilding)
    {
        uniqueBuildingIDHolder.listOfUnbuiltBuildingsUniqueIDs.Add("Unbuilt_" + unbuiltBuilding.buildingData.ID + "_" + uniqueBuildingIDHolder.listOfUnbuiltBuildingsUniqueIDs.Count, unbuiltBuilding);
        return "Unbuilt_" + unbuiltBuilding.buildingData.ID + "_" + (uniqueBuildingIDHolder.listOfUnbuiltBuildingsUniqueIDs.Count - 1);
    }

    public object ReturnBuildingObjectByUniqueID(object building, string uniqueID)
    {
        print(building.GetType());
        if(typeof(SimpleCrafter).IsAssignableFrom(building.GetType()))
        {
            Debug.Log("building is a Simple Crafter");
            SimpleCrafter temp;
            if(uniqueBuildingIDHolder.listOfSimpleCrafterUniqueIDs.TryGetValue(uniqueID,out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogError("Could not find building with unique ID " + uniqueID + " in UniqueBuildingIDHolder.ListOfSimpleCrafterUniqueIDs");
            }
            
        }

        if (typeof(Generator).IsAssignableFrom(building.GetType()))
        {
            Debug.Log("building is a Simple Crafter");
            Generator temp;
            if (uniqueBuildingIDHolder.listOfGeneratorUniqueIDs.TryGetValue(uniqueID, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogError("Could not find building with unique ID " + uniqueID + " in UniqueBuildingIDHolder.ListOfGeneratorUniqueIDs");
            }

        }

        if (typeof(BasicBuilding).IsAssignableFrom(building.GetType()))
        {
            Debug.Log("building is a Basic Building");
            BasicBuilding temp;
            if (uniqueBuildingIDHolder.listOfBasicBuildingsUniqueIDs.TryGetValue(uniqueID, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogError("Could not find building with unique ID " + uniqueID + " in UniqueBuildingIDHolder.ListOfBasicBuildingsUniqueIDs");
            }

        }

        if (typeof(InventoryBuilding).IsAssignableFrom(building.GetType()))
        {
            Debug.Log("building is a Inventory Building");
            InventoryBuilding temp;
            if (uniqueBuildingIDHolder.listOfInventoryBuildingsUniqueIDs.TryGetValue(uniqueID, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogError("Could not find building with unique ID " + uniqueID + " in UniqueBuildingIDHolder.ListOfInventoryBuildingsUniqueIDs");
            }

        }

        if (typeof(UnbuiltBuilding).IsAssignableFrom(building.GetType()))
        {
            Debug.Log("building is an unbuilt building ");
            UnbuiltBuilding temp;
            if (uniqueBuildingIDHolder.listOfUnbuiltBuildingsUniqueIDs.TryGetValue(uniqueID, out temp))
            {
                return temp;
            }
            else
            {
                Debug.LogError("Could not find building with unique ID " + uniqueID + " in UniqueBuildingIDHolder.ListOfUnbuiltBuildingsUniqueIDs");
            }

        }

        Debug.LogError("Could not find Dictionary of type " + building.GetType());
        return null;
    }

    public void LoadBuildings(object reciever, System.EventArgs args)
    {
        Debug.Log("Started loading buildings into world");
        SavedBuildingsList savedBuildingsList = SaveLists.Instance.savedBuildingList;

        for (int i = 0; i < savedBuildingsList.savedBasicBuildings.Count; i++)
        {
            Vector2 position = new Vector2(savedBuildingsList.savedBasicBuildings[i].posX, savedBuildingsList.savedBasicBuildings[i].posY);
            GameObject go = Instantiate(new GameObject(), position, Quaternion.identity);
            SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
            BasicBuilding basicBuilding = go.AddComponent<BasicBuilding>();

            basicBuilding.buildingData = FindBuildingByID(savedBuildingsList.savedBasicBuildings[i].buildingID);
            rend.sprite = basicBuilding.buildingData.sprite;
            //basicBuilding.Initialise();
            //buildingUniqueIDs.Add(basicBuilding.uniqueBuildingID);
        }

        for (int i = 0; i < savedBuildingsList.savedCrafterBuildings.Count; i++)
        {
            Vector2 position = new Vector2(savedBuildingsList.savedCrafterBuildings[i].posX, savedBuildingsList.savedCrafterBuildings[i].posY);
            GameObject go = Instantiate(new GameObject(), position, Quaternion.identity);
            SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
            BoxCollider2D col = go.AddComponent<BoxCollider2D>();
            col.size = new Vector2(2, 2);
            SimpleCrafter crafterBuilding = go.AddComponent<SimpleCrafter>();

            crafterBuilding.buildingData = FindBuildingByID(savedBuildingsList.savedCrafterBuildings[i].buildingID);
            rend.sprite = crafterBuilding.buildingData.sprite;
            //crafterBuilding.Initialise();
            //buildingUniqueIDs.Add(crafterBuilding.uniqueBuildingID);
        }

        for (int i = 0; i < savedBuildingsList.savedInventoryBuildings.Count; i++)
        {
            Vector2 position = new Vector2(savedBuildingsList.savedInventoryBuildings[i].posX, savedBuildingsList.savedInventoryBuildings[i].posY);
            GameObject go = Instantiate(new GameObject(), position, Quaternion.identity);
            SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
            InventoryBuilding inventoryBuilding = go.AddComponent<InventoryBuilding>();

            inventoryBuilding.buildingData = FindBuildingByID(savedBuildingsList.savedInventoryBuildings[i].buildingID);
            rend.sprite = inventoryBuilding.buildingData.sprite;
            //inventoryBuilding.Initialise();
            //buildingUniqueIDs.Add(inventoryBuilding.uniqueBuildingID);
        }

        for (int i = 0; i < savedBuildingsList.savedUnbuiltBuildings.Count; i++)
        {
            Vector2 position = new Vector2(savedBuildingsList.savedUnbuiltBuildings[i].posX, savedBuildingsList.savedUnbuiltBuildings[i].posY);
            GameObject go = Instantiate(new GameObject(), position, Quaternion.identity);
            SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
            UnbuiltBuilding unbuiltBuilding = go.AddComponent<UnbuiltBuilding>();

            unbuiltBuilding.buildingData = FindBuildingByID(savedBuildingsList.savedUnbuiltBuildings[i].buildingID);
            rend.sprite = unbuiltBuilding.buildingData.unbuiltSprite;
            unbuiltBuilding.resourceRequirementUIGo = unbuitBuildingResourceUIGO;
            unbuiltBuilding.BuildingPlaced();
            //unbuiltBuilding.Initialise();
            //buildingUniqueIDs.Add(unbuiltBuilding.buildingData.uniqueBuildingID);
        }
    }
}

public struct UniqueBuildingIDHolder
{
    public Dictionary<string, SimpleCrafter> listOfSimpleCrafterUniqueIDs;
    public Dictionary<string, Generator> listOfGeneratorUniqueIDs;
    public Dictionary<string, BasicBuilding> listOfBasicBuildingsUniqueIDs;
    public Dictionary<string, InventoryBuilding> listOfInventoryBuildingsUniqueIDs;
    public Dictionary<string, UnbuiltBuilding> listOfUnbuiltBuildingsUniqueIDs;

    public void Initialise()
    {
        listOfSimpleCrafterUniqueIDs = new Dictionary<string, SimpleCrafter>();
        listOfGeneratorUniqueIDs = new Dictionary<string, Generator>();
        listOfBasicBuildingsUniqueIDs = new Dictionary<string, BasicBuilding>();
        listOfInventoryBuildingsUniqueIDs = new Dictionary<string, InventoryBuilding>();
        listOfUnbuiltBuildingsUniqueIDs = new Dictionary<string, UnbuiltBuilding>();
    }
}
