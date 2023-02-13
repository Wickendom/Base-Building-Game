using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour {
    public ItemData item; //the item this cell is currently holding. THIS NEEDS SETTING PRIVATE AND ONLY NEEDS ACCESSING THROUGH FUNCTIONS TO STOP UI UPDATE ISSUES.
    private Image image;
    private Button button;
    private BoxCollider2D col;
    public Text buttonText;
    public Text stackCountUI;
    public int itemsInStack = 0;
    public bool hasItem;

    [HideInInspector]
    public bool isInput, isOutput, isFuel;

    public SimpleCrafter crafter;

    private void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        col = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        
    }

    public void UpdateCellUI(ItemData itemData, bool updateStackUI)
    {
        //button.onClick.AddListener(useableItem.Use);
        if(itemData != null)
        {
            buttonText.text = itemData.itemName;

            if (updateStackUI)
            {
                if (stackCountUI.enabled == false)
                    stackCountUI.enabled = true;
                stackCountUI.text = itemsInStack.ToString();
            }

            if(itemsInStack <= 1)
            {
                stackCountUI.enabled = false;
            }
        }
        else
        {
            buttonText.text = "Empty";

            if (updateStackUI)
            {
                if (stackCountUI.enabled == true)
                    stackCountUI.enabled = false;
                stackCountUI.text = itemsInStack.ToString();
                
            }
        }
    }

    public void AddItem(ItemData Item)
    {
        if (item == null)
        {
            item = Item;
            itemsInStack = 1;
            UpdateCellUI(item, false);
        }
        else if(item == Item)
        {
            itemsInStack++;
            UpdateCellUI(item, true);
        }
        else if(item != Item)
        {
            Debug.Log("Items do not match");
        }
        hasItem = true;
    }

    public void AddItem(ItemData Item, int itemAmount)
    {
        if (item == null)
        {
            item = Item;
            itemsInStack = itemAmount;
            UpdateCellUI(item, (itemAmount >= 2)?true:false);
        }
        else if (item != Item)
        {
            Debug.Log("Items do not match");
        }
        hasItem = true;
    }

    public bool CheckIfItemIsACompatibleInputItem(InventoryCell swapWithCell)//this checks to see if the item put into a crafters input slot is able to be refined by the building
    {
        if (isOutput)
        {
            return false;
        }
        else if(isInput)
        {
            CrafterBuildingData temp = crafter.buildingData as CrafterBuildingData;

            for (int i = 0; i < temp.itemsBuildingIsAbleToRefine.Length; i++)
            {
                if(swapWithCell.item == temp.itemsBuildingIsAbleToRefine[i])
                {
                    crafter.UIHolder.inputItem = swapWithCell.item;
                    crafter.UIHolder.inputItemsInStack = swapWithCell.itemsInStack;
                    SwapItems(swapWithCell);
                    return true;
                }
            }

            return false;
        }
        else if (isFuel)
        {
            CrafterBuildingData temp = crafter.buildingData as CrafterBuildingData;

            for (int i = 0; i < temp.fuelItems.Length; i++)
            {
                if (swapWithCell.item == temp.fuelItems[i])
                {
                    crafter.UIHolder.fuelItem = swapWithCell.item;
                    crafter.UIHolder.fuelItemsInStack = swapWithCell.itemsInStack;
                    SwapItems(swapWithCell);
                    return true;
                }
            }

            return false;
        }
        else
        {
            SwapItems(swapWithCell);
            return true;
        }
    }
    
    private void SwapItems(InventoryCell swapWithCell)
    {
        ItemData tempItem = swapWithCell.item;
        int tempItemStack = swapWithCell.itemsInStack;

        ////TO DO: ADD RESTRICTIONS ON CELLS SO THEY CAN ONLY ADD CERTAIN ITEMS E.G. SMELTERY WILL ONLY ACCEPT COAL/ORE.

        swapWithCell.SetItem(item, itemsInStack);
        SetItem(tempItem, tempItemStack);
    }

    public void SetItem(ItemData Item, int itemAmount)
    {
        item = Item;
        itemsInStack = itemAmount;
        hasItem = (item != null) ? true : false; 
        UpdateCellUI(item, true);
    }

    public void RemoveItem()
    {
        if(itemsInStack > 1)
        {
            itemsInStack--;
            UpdateCellUI(item,true);
        }
        else if(itemsInStack == 1)
        {
            item = null;
            UpdateCellUI(item, true);
            hasItem = false;
        }
    }

    public void DisableCell()
    {
        image.enabled = false;
        button.enabled = false;
        col.enabled = false;
        buttonText.enabled = false;
    }

    public void EnableCell()
    {
        image.enabled = true;
        button.enabled = true;
        col.enabled = true;
        buttonText.enabled = true;
    }
}
