using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryBuilding : Building {

    private InventoryBuildingData inventoryBuildingData;
    public List<InventoryCell> cells;

    public InventoryBuildingUILayout UILayout;
    public bool UIOpen;

    public override void Initialise()
    {
        buildingData.Initialise(gameObject);

        buildingID = buildingData.ID;
        gameObject.tag = "BuildingWithUI";
        uniqueBuildingID = BuildingManager.Instance.AddNewUniqueID(this);
        cells = new List<InventoryCell>();
        inventoryBuildingData = buildingData as InventoryBuildingData;
    }

    public void OnEnable()
    {
        SaveLists.SaveEvent += SaveBuildingData;
    }

    public override void OpenBuildingUI()
    {
        UIOpen = true;
        UIController.Instance.OpenBuildingUI(this, UILayout);
    }

    public void OnDisable()
    {
        SaveLists.SaveEvent -= SaveBuildingData;
    }

    public void SaveBuildingData(object sender, EventArgs args)
    {
        SavedInventoryBuilding savedInventoryBuilding = new SavedInventoryBuilding();

        savedInventoryBuilding.buildingID = buildingID;
        savedInventoryBuilding.posX = transform.position.x;
        savedInventoryBuilding.posY = transform.position.y;

        for (int i = 0; i < cells.Count; i++)
        {
            savedInventoryBuilding.itemsInInventory.Add(cells[i].item.ID);
            savedInventoryBuilding.itemStackAmount.Add(cells[i].itemsInStack);
        }

        SaveLists.Instance.GetBuildingList().savedInventoryBuildings.Add(savedInventoryBuilding);
    }

    public void GetLocalItems(BuildingUI buildingUI)
    {
        ItemData[] items = new ItemData[cells.Count];
        int[] itemsStacks = new int[cells.Count];

        for (int i = 0; i < cells.Count; i++)
        {
            items[i] = cells[i].item;
            itemsStacks[i] = cells[i].itemsInStack;
        }

        buildingUI.SetInventoryCellsItems(items, itemsStacks);
    }
}
