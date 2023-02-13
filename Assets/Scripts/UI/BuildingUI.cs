using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : UIScreenBase {

    private RectTransform rectTransform;
    private Image image;

    public BuildingUILayout UILayout;
    public List<GameObject> inventoryCellObjects;

    private List<InventoryCell> inputCells;
    private List<InventoryCell> outputCells;
    private InventoryCell fuelCell;

    private string currentBuildingIDUIOpened;
    private SimpleCrafter simpleCrafter;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        UIController.Instance.buildingUI = this;
        backgroundUI = UIController.Instance.UIBackground;
        inventoryCellObjects = new List<GameObject>();

        inputCells = new List<InventoryCell>();
        outputCells = new List<InventoryCell>();
    }

    void DestroyOldUI()
    {
        for (int i = 0; i < inventoryCellObjects.Count; i++)
        {
            Destroy(inventoryCellObjects[i]);
        }
    }

    public void OpenBuildingUI(object building, BuildingUILayout uiLayout)
    {
        if(uiLayout.cellAmount > inventoryCellObjects.Count)
        {
            int amountToCreate = uiLayout.cellAmount - inventoryCellObjects.Count;

            for (int i = 0; i < amountToCreate; i++)
            {
                GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, transform);
                inventoryCellObjects.Add(cell);
            }

        }

        Building tempGO = (Building)building;
        tempGO.SendMessage("GetLocalItems",this);

        ///NEEDS UPDATING TO ALLOW BUILDINGS WITH MORE THAN 1 INPUT/OUTPUT///
        ///THIS NEEDS TO SET INPUT/OUTPUT CELLS FROM THE UI LAYOUT///
        ///YOU ALSO NEED TO SET ALL ITEMS FROM THE BUILDING HERE///
        if(typeof(SimpleCrafter).IsAssignableFrom(building.GetType()))
        {
            simpleCrafter = BuildingManager.Instance.ReturnBuildingObjectByUniqueID(building, tempGO.uniqueBuildingID) as SimpleCrafter;
            inputCells.Clear();
            outputCells.Clear();

            for (int i = 0; i < uiLayout.amountOfInputSlots; i++)
            {
                inputCells.Add(null);
            }
            for (int i = 0; i < uiLayout.amountOfOutputSlots; i++)
            {
                outputCells.Add(null);
            }
            
            /*inputCells = new List<InventoryCell>(uiLayout.amountOfInputSlots);
            outputCells = new List<InventoryCell>(uiLayout.amountOfOutputSlots);*/

            for (int i = 0; i < uiLayout.amountOfInputSlots; i++)
            {
                inventoryCellObjects[i].GetComponent<InventoryCell>().isInput = true;
                inputCells[i] = inventoryCellObjects[i].GetComponent<InventoryCell>();
                inputCells[i].crafter = simpleCrafter;
            }
            for (int i = 0; i < uiLayout.amountOfOutputSlots; i++)
            {
                inventoryCellObjects[i+uiLayout.amountOfInputSlots].GetComponent<InventoryCell>().isOutput = true;
                outputCells[i] = inventoryCellObjects[i+uiLayout.amountOfInputSlots].GetComponent<InventoryCell>();
                outputCells[i].crafter = simpleCrafter;
            }

            if(simpleCrafter.crafterBuildingData.requiresFuel)
            {
                inventoryCellObjects[inventoryCellObjects.Count - 1].GetComponent<InventoryCell>().isFuel = true;
                inventoryCellObjects[inventoryCellObjects.Count - 1].GetComponent<InventoryCell>().crafter = simpleCrafter;
                fuelCell = inventoryCellObjects[inventoryCellObjects.Count - 1].GetComponent<InventoryCell>();

            }

            simpleCrafter.inputCells = inputCells;
            simpleCrafter.outputCells = outputCells;
            simpleCrafter.fuelCell = fuelCell;
        }

        if(typeof(Generator).IsAssignableFrom(building.GetType()))
        {
            outputCells.Clear();
            outputCells.Add(null);

            outputCells[0] = inventoryCellObjects[0].GetComponent<InventoryCell>();
            Generator generator = BuildingManager.Instance.ReturnBuildingObjectByUniqueID(building, tempGO.uniqueBuildingID) as Generator;

            generator.outputCell = inventoryCellObjects[0].GetComponent<InventoryCell>();
        }
    }

    ///OLD VERSION TO OPEN BUILDING UI. DELETE LATER WHEN NOT NEEDED
    /*public void OpenBuildingUI(GameObject crafter, CrafterUIHolder UIHolder)
    {
        UILayout = UIHolder.UILayout;
        simpleCrafter = crafter.GetComponent<SimpleCrafter>();

        if (inventoryCellObjects.Count == 0)//creates the simple crafter UI cells on the first time opening the window.
        {
            //DestroyOldUI();
            for (int i = 0; i < UILayout.cellAmount; i++)
            {
                GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, transform);

                if (i == 0)
                {
                    inputCells = cell.GetComponent<InventoryCell>();
                    inputCells.isInput = true;
                    
                }
                else if (i == 1)
                {
                    outputCells = cell.GetComponent<InventoryCell>();
                    outputCells.isOutput = true;
                    
                }

                inventoryCellObjects.Add(cell);
            }
        }
        if(currentBuildingIDUIOpened != null)
        {
            print("current id is not null");
            SimpleCrafter temp = BuildingManager.Instance.ReturnBuildingObjectByUniqueID(crafter.GetComponent<SimpleCrafter>(), currentBuildingIDUIOpened) as SimpleCrafter;
            print(temp.GetType());
        }
        currentBuildingIDUIOpened = simpleCrafter.uniqueBuildingID;

        //// Set input item when the building ui gets opened. This needs setting from the building.////
        
        inputCells.crafter = simpleCrafter;
        outputCells.crafter = simpleCrafter;
    }*/

    public void SetInventoryCellsItems(ItemData[] items, int[] itemStackAmounts)
    {
        for (int i = 0; i < inventoryCellObjects.Count; i++)
        {
            InventoryCell tempCell = inventoryCellObjects[i].GetComponent<InventoryCell>();
            if(i >= items.Length)
            {
                tempCell.SetItem(null,0);
            }
            else
            {
                tempCell.SetItem(items[i], itemStackAmounts[i]);
            }
        }
    }

    /*public void SetInputItem(ItemData item, int itemStackAmount)
    {
        
        inputCells.itemsInStack = itemStackAmount;

        if (item != null)
        {
            inputCells.item = item;
            inputCells.hasItem = true;
            inputCells.UpdateCellUI(item, true);
        }
        else
        {
            inputCells.UpdateCellUI(null, true);
            inputCells.hasItem = false;
        }  
    }

    public void SetOutputItem(ItemData item, int itemStackAmount)
    {

        outputCells.itemsInStack = itemStackAmount;

        if (item != null)
        {
            outputCells.item = item;
            outputCells.hasItem = true;
            outputCells.UpdateCellUI(item, true);
        }
        else
        {
            outputCells.UpdateCellUI(null, true);
            outputCells.hasItem = false;
        }
    }*/

    public void CloseUI()
    {
        simpleCrafter.CloseBuildingUI();
        simpleCrafter = null;
    }
}
