using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMove))]
public class Player : Character
{

    static public Player Instance;

    public int playerLevel = 1;
    public int curExp = 0;
    private int expToNextLevel = 50;

    public delegate void AddExp(int amount);
    public static AddExp addExp;

    public PlayerInput playerInput;
    public PlayerMove playerMove;
    public InventoryControl inventory;

    Vector2 inputDirection;

    private bool canMine = true;

    public LayerMask resourceLayerMask;

    //public Tool equipedTool;

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

        playerInput = GetComponent<PlayerInput>();
        playerMove = GetComponent<PlayerMove>();
        inventory = GetComponent<InventoryControl>();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        addExp = OnAddExp; //this sets the onAddExp as the functions that runs when the addExp variable is called.
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && canMine)
        {
            StartCoroutine(Mine());
        }
    }

    /*public void EquipTool(Tool tool)
    {
        print("tool equiped");
        equipedTool = tool;
        equipedTool.CheckTile();
    }*/

    private IEnumerator Mine()
    {
        float mineTime = 0.5f;
        ItemData currentItem = QuickbarController.Instance.ReturnSelectedItemData();
        if (currentItem != null)
        {
            if (currentItem.isTool)
            {
                mineTime = 0.7f;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(0, 0), 0f, resourceLayerMask);

        if (hit)
        {
            ItemData minedItem = hit.collider.GetComponent<Resouce>().itemData;
            inventory.AddItemToInventory(minedItem);
            addExp(10);
        }
        canMine = false;

        yield return new WaitForSeconds(mineTime);
        canMine = true;
    }

    public void BeginMove(Vector2 inputDirection)
    {
        playerMove.Move(inputDirection, moveSpeed);
    }

    public void PickUpItem(ItemData itemData)
    {
        //print(playerInventory.gameObject.name);
        inventory.AddItemToInventory(itemData);
    }

    private void OnAddExp(int amount)
    {
        curExp += amount;

        if(curExp>= expToNextLevel)
        {
            playerLevel++;
            curExp = curExp - expToNextLevel;
            ResearchController.addResearchPoint();
        }
    }
}
