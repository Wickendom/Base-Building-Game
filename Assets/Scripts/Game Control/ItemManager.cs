using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public static ItemManager Instance;

    public List<ItemData> fullItemList;
    public Item defaultItem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddItemSlot()
    {
        fullItemList.Add(null);
    }

    public void RemoveItem(int index)
    {
        fullItemList.RemoveAt(index);
    }

    public ItemData FindItemByID(string itemID)
    {
        for (int i = 0; i < fullItemList.Count; i++)
        {
            if(itemID == fullItemList[i].ID)
            {
                return fullItemList[i];
            }
        }
        return null;
    }

    /*public bool CheckItemIdIsSame(int ID, ItemData item)
    {
        if(ID <= fullItemList.Count)
        {
            if(item.itemName == fullItemList[ID].itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void SetIDToCorrectItem(ref ItemData item)
    {
        bool foundItem = false;
        for (int i = 0; i < fullItemList.Count; i++)
        {
            if(item.itemName == fullItemList[i].itemName)
            {
                item.ID = i;
                foundItem = true;
            }
        }
        if(foundItem == false)
        {
            Debug.LogError("A matching item was not found when setting");
        }
    }*/
}
