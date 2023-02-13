using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

    public static InventoryControl Instance;

    public GameObject tool;

    public PlayerInventory[] inventories = new PlayerInventory[2];

    InventoryCell transferFromCell;

    [SerializeField]
    GameObject UIObjectToFollowMouse;

    [SerializeField]
    private LayerMask UIButtonMask;

    [SerializeField]
    private bool isDraggingItem;

    public RectTransform canvas;

    public SaveLists saveLists;


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

    public void Start()
    {
        saveLists = SaveLists.Instance;
    }

    private void OnEnable()
    {
        SaveLists.LoadEvent += LoadInventories;
    }

    private void OnDisable()
    {
        SaveLists.LoadEvent -= LoadInventories;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(0, 0), 0f, UIButtonMask);

            if (hit)
            {
                isDraggingItem = true;
                transferFromCell = hit.collider.GetComponent<InventoryCell>();
                UIObjectToFollowMouse = Instantiate(hit.collider.gameObject, new Vector3(Input.mousePosition.x,Input.mousePosition.y, 4.1f), Quaternion.identity, canvas);
                InventoryCell tempCell = UIObjectToFollowMouse.GetComponent<InventoryCell>();
                InventoryCell hitCell = hit.collider.GetComponent<InventoryCell>();
                tempCell.item = hitCell.item;
                tempCell.itemsInStack = hitCell.itemsInStack;
            }
        }

        if (isDraggingItem && UIObjectToFollowMouse != null)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 4.1f;
            UIObjectToFollowMouse.transform.position = Camera.main.ScreenToWorldPoint(pos);
        }

        if(Input.GetMouseButtonUp(0) && isDraggingItem)
        {
            isDraggingItem = false;

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(0, 0), 0f, UIButtonMask);

            if (hit)
            {
                InventoryCell transferToCell = hit.collider.GetComponent<InventoryCell>();
                transferToCell.CheckIfItemIsACompatibleInputItem(transferFromCell);

                if(transferFromCell.item != null)
                {
                    transferToCell.UpdateCellUI(transferToCell.item, true);
                    transferFromCell.UpdateCellUI(transferFromCell.item, true);
                }
                else
                {
                    transferToCell.UpdateCellUI(transferToCell.item, true);
                    transferFromCell.UpdateCellUI(null, true);
                }
                
            }

            Destroy(UIObjectToFollowMouse.gameObject);
            UIObjectToFollowMouse = null;
            transferFromCell = null;
        }
    }

    public void AddItemToInventory(ItemData itemData)
    {
        if(inventories[0].itemsInInventory < inventories[0].cells.Count)
        {
            inventories[0].AddItem(itemData);
        }
        else
        {
            inventories[1].AddItem(itemData);
        }
    }

    public bool AddItemToUnbuiltBuilding(ItemData item)
    {

        for (int i = 0; i < inventories.Length; i++)
        {
            for (int j = 0; j < inventories[i].cells.Count; j++)
            {
                if (inventories[i].cells[j].item == item)
                {
                    RemoveItem(i, j);
                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckItem(ItemData item, int itemAmount)
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            for (int j = 0; j < inventories[i].cells.Count; j++)
            {
                if (inventories[i].cells[j].item == item && inventories[i].cells[j].itemsInStack >= itemAmount)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void RemoveItem(int inventoryIndex, int cellIndex)
    {
        if(inventories[inventoryIndex].cells[cellIndex].itemsInStack > 1)
        {
            inventories[inventoryIndex].cells[cellIndex].itemsInStack--;
            inventories[inventoryIndex].cells[cellIndex].stackCountUI.text = inventories[inventoryIndex].cells[cellIndex].itemsInStack.ToString();
        }
        else
        {
            inventories[inventoryIndex].cells[cellIndex].itemsInStack--;
            inventories[inventoryIndex].cells[cellIndex].item = null;
            inventories[inventoryIndex].cells[cellIndex].stackCountUI.text = "";
            inventories[inventoryIndex].cells[cellIndex].buttonText.text = "Empty";
        }
    }

    public void RemoveItem(ItemData item, int amountToRemove)
    {
        int amountLeftToRemove = amountToRemove;

        for (int i = 0; i < inventories.Length; i++)
        {
            for (int y = 0; y < inventories[i].cells.Count; y++)
            {
                if(inventories[i].cells[y].item == item)
                {
                    int itemscurrentlyInStack = inventories[i].cells[y].itemsInStack;

                    for (int z = 0; z < itemscurrentlyInStack; z++)
                    {
                        if (amountLeftToRemove > 0)
                        {
                            amountLeftToRemove--;
                            RemoveItem(i, y);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    public void LoadInventories(object reciever, System.EventArgs args)
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            for (int j = 0; j < inventories[i].cells.Count; j++)
            {
                inventories[i].cells[j].item = ItemManager.Instance.FindItemByID(saveLists.savedInventoryList.savedInventories[i].item[j]);
                inventories[i].cells[j].itemsInStack = saveLists.savedInventoryList.savedInventories[i].itemsInStack[j];
                inventories[i].UpdateCellUIFromCellData(j);
            } 
        }
    }
}
