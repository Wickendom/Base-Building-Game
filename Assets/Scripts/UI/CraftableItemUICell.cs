using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemUICell : MonoBehaviour {

    public ItemData itemData;

    bool CheckCanCraftItem()
    {
        bool[] haveResources = new bool[itemData.itemsRequiredToCraft.Length];
        for (int i = 0; i < itemData.itemsRequiredToCraft.Length; i++)
        {
            if(InventoryControl.Instance.CheckItem(itemData.itemsRequiredToCraft[i], itemData.resourceAmount[i]))
            {
                haveResources[i] = true;
            }
            else
            {
                return false;
            }
        }

        for (int i = 0; i < haveResources.Length; i++)
        {
            if(haveResources[i] == false)
            {
                return false;
            }

            
        }
        return true;
    }

    public void CraftItem()
    {
        if(CheckCanCraftItem())
        {
            for (int i = 0; i < itemData.itemsRequiredToCraft.Length; i++)
            {
               
                InventoryControl.Instance.RemoveItem(itemData.itemsRequiredToCraft[i], itemData.resourceAmount[i]);
            }
            
            InventoryControl.Instance.AddItemToInventory(itemData);
        }
    }

}
