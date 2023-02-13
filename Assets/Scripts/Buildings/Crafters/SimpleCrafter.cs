using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class SimpleCrafter : Building {

    public CrafterBuildingData crafterBuildingData;

    public BuildingUI buildingUI;
    public CrafterUILayout UILayout;

    public CrafterUIHolder UIHolder;

    public List<InventoryCell> inputCells;
    public List<InventoryCell> outputCells;
    public InventoryCell fuelCell;

    private float fuelLastingTime;
    private bool hasFuel;

    private bool crafting;

    public bool UIOpen;


    public override void Start()
    {
        crafterBuildingData = buildingData as CrafterBuildingData;
        buildingUI = UIController.Instance.buildingUI;
        Initialise();
    }

    public void OnEnable()
    {
        SaveLists.SaveEvent += SaveBuildingData;
    }

    public void OnDisable()
    {
        SaveLists.SaveEvent -= SaveBuildingData;
    }

    public override void Initialise()
    {
        buildingData.Initialise(gameObject);
        gameObject.tag = "BuildingWithUI";
        buildingID = buildingData.ID;
        print(gameObject.name + " called Initialise func");
        uniqueBuildingID = BuildingManager.Instance.AddNewUniqueID(this);

        UIHolder.inputItem = null;
        UIHolder.inputItemsInStack = 0;

        UIHolder.outputItem = null;
        UIHolder.outputItemsInStack = 0;

        UIHolder.fuelItem = null;
        UIHolder.fuelItemsInStack = 0;

        UIHolder.UILayout = UILayout;
    }

    private void Update()
    {
        if(UIHolder.inputItem != null)
        {
            if (!crafting)
            {
                StartCoroutine("BeginCrafting");
            }
        }

        if (hasFuel)
        {
            fuelLastingTime -= Time.deltaTime;
            if(fuelLastingTime <= 0)
            {
                hasFuel = false;
            }
        }
    }

    public IEnumerator BeginCrafting()
    {
        if(crafterBuildingData.requiresFuel)
        {
            if(!hasFuel)
            {
                bool requiredFuelItemFound = false;
                for (int i = 0; i < crafterBuildingData.fuelItems.Length; i++)
                {
                    if (crafterBuildingData.fuelItems[i] == UIHolder.fuelItem)
                    {
                        print("compatible fuel source found");
                        requiredFuelItemFound = true;

                        hasFuel = true;
                        fuelLastingTime = UIHolder.fuelItem.fuelBurnTime;
                        RemoveFuelItem();
                    }
                }
                if (requiredFuelItemFound == false)
                {
                    yield break;
                }
            }
        }

        crafting = true;
        ItemData refinedItem = RefineItem(UIHolder.inputItem);
        float time = UIHolder.inputItem.timeToRefine;

        UIHolder.inputItemsInStack--;

        if(UIHolder.inputItemsInStack <= 0)
        {
            UIHolder.inputItem = null;
        }
        if(UIOpen)
        {
            inputCells[0].RemoveItem();// CHANGE THIS TO ADD MULTIPLE INPUT SLOT CHECKING
        }
        //SetLocalCopyOfCellData(0, inputCell.item, inputCell.itemsInStack);

        yield return new WaitForSeconds(time);
     
        UIHolder.outputItem = refinedItem;
        UIHolder.outputItemsInStack++;
        outputCells[0].AddItem(refinedItem);//CHANGE THIS TO ALLOW 2 OUTPUT SLOTS
        //SetLocalCopyOfCellData(1, outputCell.item, outputCell.itemsInStack);
        crafting = false;
    }

    private void RemoveFuelItem()
    {
        UIHolder.fuelItemsInStack--;

        if (UIHolder.fuelItemsInStack <= 0)
        {
            UIHolder.fuelItem = null;
        }
        if (UIOpen)
        {
            fuelCell.RemoveItem();
        }
    }


    private ItemData RefineItem(ItemData inputItem)
    {
        return inputItem.refinedItemOutput;
    }

    public override void OpenBuildingUI()
    {
        UIOpen = true;
        UIController.Instance.OpenBuildingUI(this, UILayout);
    }

    public void CloseBuildingUI()
    {
        UIOpen = false;
    }

    public void SaveBuildingData(object sender, EventArgs args)
    {
        SavedCrafterBuilding savedCrafterBuilding = new SavedCrafterBuilding();
        
        savedCrafterBuilding.buildingID = buildingID;
        savedCrafterBuilding.posX = transform.position.x;
        savedCrafterBuilding.posY = transform.position.y;

        SaveLists.Instance.savedBuildingList.savedCrafterBuildings.Add(savedCrafterBuilding);
    }

    public void GetLocalItems(BuildingUI buildingUI)
    {
        ItemData[] items = new ItemData[2];

        items[0] = UIHolder.inputItem;
        items[1] = UIHolder.outputItem;

        int[] itemsStacks = new int[2];

        itemsStacks[0] = UIHolder.inputItemsInStack;
        itemsStacks[1] = UIHolder.outputItemsInStack;

        buildingUI.SetInventoryCellsItems(items, itemsStacks);
    }
}

[System.Serializable]
public struct CrafterUIHolder
{
    public ItemData inputItem;
    public int inputItemsInStack;

    public ItemData outputItem;
    public int outputItemsInStack;

    public ItemData fuelItem;
    public int fuelItemsInStack;

    public CrafterUILayout UILayout;
}
