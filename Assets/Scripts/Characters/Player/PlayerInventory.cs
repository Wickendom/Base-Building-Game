using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : UIScreenBase {

    public bool isQuickBar;

    public int itemsInInventory = 0;

    public List<InventoryCell> cells;

    public RectTransform backgroundTransform;

    public override void Start()
    {
        base.Start();
        if (isQuickBar)
        {
            UIController.Instance.quickBarUI = this;
        }
        else
        {
            UIController.Instance.playerInventory = this;
        }
        CreateInventoryList();
    }

    private void OnEnable()
    {
        SaveLists.SaveEvent += SaveFunction;
    }

    private void OnDisable()
    {
        SaveLists.SaveEvent -= SaveFunction;
    }

    public void AddItem(ItemData itemData)
    {
        for (int i = 0; i < cells.Count - 1; i++)
        {
            if(cells[i].item == null)
            {
                cells[i].AddItem(itemData);
                cells[i].itemsInStack = 1;
                cells[i].UpdateCellUI(itemData, cells[i].item.isStackable ? true:false);
                break;
            }
            else if(cells[i].item == itemData && itemData.isStackable == true && cells[i].itemsInStack < 32)
            {
                cells[i].itemsInStack++;
                cells[i].UpdateCellUI(itemData, true);
                break;
            }
        }
    }

    public void RemoveItem(int itemIndex)
    {

    }

    public void CreateInventoryList()
    {
        cells = new List<InventoryCell>();

        backgroundTransform = UIController.Instance.UIBackground;
        RectTransform quickBarBackgroundTransform = GetComponent<RectTransform>();

        if (isQuickBar)
        {
            
            bgWidth = (cellRowAmount * cellWidth) + ((cellRowAmount + 1) * cellPadding);
            bgHeight = (cellCollumnAmount * cellHeight) + ((cellCollumnAmount + 1) * cellPadding);  
            //Vector3 temp = new Vector3(bgWidth / 2, bgHeight / 2, 0);
            quickBarBackgroundTransform.sizeDelta = new Vector2(bgWidth,bgHeight);
            //backgroundUI.position += new Vector3((isQuickBar?0:bgWidth / 2) + cellPadding,(isQuickBar?0:-bgHeight / 2) - cellPadding,0);
        }

        Vector3 cellUIPos = new Vector3((-bgWidth / 2) + (cellWidth / 2) + cellPadding, (bgHeight / 2) - (cellHeight / 2) - cellPadding, 0);

        for (int y = 0; y < cellCollumnAmount * cellRowAmount; y++)
        {
            
            GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, !isQuickBar?transform:quickBarBackgroundTransform);
            RectTransform rectTemp = cell.GetComponent<RectTransform>();
            rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);

            if(isQuickBar)
            {
                cell.transform.localPosition =  cellUIPos;
                cellUIPos.x += cellWidth + cellPadding;
            }

            cells.Add(cell.GetComponent<InventoryCell>());
            if(!isQuickBar)
            cell.GetComponent<InventoryCell>().DisableCell();
            
        }

        if (isQuickBar)
        {
            GetComponent<QuickbarController>().Initialise();
        }
    }

    public void UpdateCellUIFromCellData(int cellIndex)
    {
        cells[cellIndex].UpdateCellUI(cells[cellIndex].item, true);
    }

    public void SaveFunction(object sender, System.EventArgs args)
    {
        print("Player Inventory saved. This one is the quickbar ? " + isQuickBar);
        SavedInventory inventory = new SavedInventory();
        for (int i = 0; i < cells.Count; i++)
        {
            inventory.cellID.Add(i);
            inventory.itemsInStack.Add(cells[i].itemsInStack);
            inventory.item.Add((cells[i].item != null) ? cells[i].item.ID:null);
        }

        SaveLists.Instance.GetInventoryLists().savedInventories.Add(inventory);
    }

    public void DisableCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].DisableCell();
        }
    }

    public void EnableCells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].EnableCell();
        }
    }
}
