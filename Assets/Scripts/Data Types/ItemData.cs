using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemData : ScriptableObject {

    public string ID;
    public string itemName = "New Item"; //  What the item will be called in the inventory
    public Sprite itemIcon = null;       //  What the item will look like in the inventory
    //public Rigidbody itemObject = null;  //  Optional slot for a PreFab to instantiate when discarding
    public bool isUnique = false;        //  Optional checkbox to indicate that there should only be one of these items per game
    public bool isQuestItem = false;     //  Examples of additional information that could be held in InventoryItem
    public bool isStackable = false;     //  Examples of additional information that could be held in InventoryItem
    public int maxStackAmount = 64;
    public bool isTool = false;
    public bool isCraftable = false;

    public ItemData[] itemsRequiredToCraft;
    public int[] resourceAmount;

    public bool destroyOnUse = false;    //  Examples of additional information that could be held in InventoryItem

    public float timeToRefine;
    public ItemData refinedItemOutput;   // The item that will be output once refined (if its an item that can be refined)

    public float fuelBurnTime;
}
