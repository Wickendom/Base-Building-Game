using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    public RectTransform UIBackground;
    private Image image;

    public BuildingUI buildingUI;
    public PlayerInventory playerInventory;
    public PlayerInventory quickBarUI;
    public CraftingUI craftingUI;
    public ResearchUI researchUI;

    [HideInInspector]
    public GameObject buildingUIGO, playerInventoryUIGO, quickBarUIGO, craftingUIGO, researchUIGO;

    private float bgWidth, bgHeight, cellWidth, cellHeight, cellPadding;

    public bool craftingUIOpen, inventoryUIOpen, researchUIOpen;

    private Vector3 cellUIPos;

    [SerializeField]
    private RectTransform inventoryTab, craftingTab, researchTab;
    private BoxCollider2D inventoryTabCollider, craftingTabCollider, researchTabCollider;

    public ItemData itemToAdd;
    private bool itemAdded;

    private List<ResearchNode> tempXNodeList;

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
        UIBackground = GetComponent<RectTransform>();
    }

    private void Start()
    {
        image = GetComponent<Image>();
        craftingUI = GetComponentInChildren<CraftingUI>();
        researchUI = GetComponentInChildren<ResearchUI>();

        inventoryTabCollider = inventoryTab.GetComponent<BoxCollider2D>();
        craftingTabCollider = craftingTab.GetComponent<BoxCollider2D>();

        tempXNodeList = new List<ResearchNode>();

        buildingUIGO = buildingUI.gameObject;
        playerInventoryUIGO = playerInventory.gameObject;
        quickBarUIGO = quickBarUI.gameObject;
        craftingUIGO = craftingUI.gameObject;
        researchUIGO = researchUI.gameObject;
        
        ClosePlayerInventoryUI();
        CloseBuildingUI();
        CloseCraftingUI();
        CloseResearchUI();
    }

    public void UpdateCraftingUI(BuildingData buildingData)
    {
        craftingUIGO.GetComponent<CraftingUI>().UpdateBuildingList(buildingData);
    }

    public void UpdateCraftingUI(ItemData itemData)
    {
        craftingUIGO.GetComponent<CraftingUI>().UpdateItemList(itemData);
    }

    private void SetTabPositions(Transform parent)
    {
        inventoryTab.sizeDelta = new Vector2(bgWidth / 3, inventoryTab.sizeDelta.y);
        inventoryTab.transform.SetParent(parent);
        inventoryTab.transform.localPosition = new Vector3(((-bgWidth / 2) + (inventoryTab.sizeDelta.x / 2)) + 5, ((bgHeight / 2) + (inventoryTab.sizeDelta.y / 2)) + 5, 0);
        inventoryTabCollider.size = inventoryTab.sizeDelta;

        craftingTab.sizeDelta = new Vector2(bgWidth / 3, craftingTab.sizeDelta.y);
        craftingTab.transform.SetParent(parent);
        craftingTab.transform.localPosition = new Vector3(((-bgWidth / 2) + (craftingTab.sizeDelta.x / 2)) + (craftingTab.sizeDelta.x + 10), ((bgHeight / 2) + (craftingTab.sizeDelta.y / 2)) + 5, 0);
        craftingTabCollider.size = craftingTab.sizeDelta;
    }

    #region OPEN/CLOSE UI's

    public void CloseAllUI()
    {
        buildingUIGO.SetActive(false);
        playerInventory.DisableCells();
        quickBarUIGO.SetActive(false);
        craftingUIGO.SetActive(false);
    }
    public void CloseOtherUI()
    {
        image.enabled = false;
        buildingUIGO.SetActive(false);
        playerInventory.DisableCells();
        craftingUIGO.SetActive(false);
    }

    /*public void OpenBuildingUI(GameObject crafter , CrafterUIHolder UIHolder)// this is the UI for in game buildings e.g. the smeltery's UI.
    {
        CloseOtherUI();
        image.enabled = true;
        buildingUIGO.SetActive(true);
        buildingUI.SetBaseUISize(500,500);
        buildingUI.SetBgValues(ref bgWidth, ref bgHeight, ref cellWidth, ref cellHeight, ref cellPadding);
        buildingUI.OpenBuildingUI(crafter, UIHolder);

        SetTabPositions(buildingUI.transform);
        cellUIPos = new Vector3((-bgWidth / 2) + (cellWidth / 2) + cellPadding, 0, 0);

        for (int i = 0; i < buildingUI.inventoryCellObjects.Count; i++)
        {
            buildingUI.inventoryCellObjects[i].transform.localPosition = cellUIPos;
            cellUIPos = new Vector3((bgWidth / 2) - (cellWidth / 2) - cellPadding, 0, 0);
        }

    }*/ 

    public void OpenBuildingUI(object building, BuildingUILayout UILayout)// this is the UI for in game buildings e.g. the smeltery's UI.
    {
        CloseOtherUI();
        image.enabled = true;
        buildingUIGO.SetActive(true);
        buildingUI.SetBaseUISize(500, 500);
        buildingUI.SetBgValues(ref bgWidth, ref bgHeight, ref cellWidth, ref cellHeight, ref cellPadding);
        buildingUI.OpenBuildingUI(building, UILayout);

        SetTabPositions(buildingUI.transform);
        cellUIPos = new Vector3((-bgWidth / 2) + (cellWidth / 2) + cellPadding, 0, 0);

        if (typeof(CrafterUILayout).IsAssignableFrom(UILayout.GetType()))
        {
            CrafterUILayout crafterUILayout = UILayout as CrafterUILayout;
            //cellUIPos = crafterUILayout.inputCellPos;
            for (int i = 0; i < UILayout.amountOfInputSlots; i++)
            {
                cellUIPos = crafterUILayout.inputCellPos;
                buildingUI.inventoryCellObjects[i].transform.localPosition = cellUIPos;
            }
            for (int i = 0; i < UILayout.amountOfOutputSlots; i++)
            {
                cellUIPos = crafterUILayout.outputCellPos;
                buildingUI.inventoryCellObjects[i + UILayout.amountOfInputSlots].transform.localPosition = cellUIPos;           
            }
            cellUIPos = crafterUILayout.fuelCellPos;
            buildingUI.inventoryCellObjects[UILayout.cellAmount - 1].transform.localPosition = cellUIPos;   

        }
        else
        {
            for (int i = 0; i < buildingUI.inventoryCellObjects.Count; i++)

            {
                buildingUI.inventoryCellObjects[i].transform.localPosition = cellUIPos;
                cellUIPos = new Vector3((bgWidth / 2) - (cellWidth / 2) - cellPadding, 0, 0);
            }
        }

    }

    public void OpenPlayerInventoryUI()
    {
        CloseOtherUI();
        inventoryUIOpen = true;
        image.enabled = true;
        playerInventoryUIGO.SetActive(true);
        playerInventory.EnableCells();
        playerInventory.SetUiSizeIncludingCells();
        playerInventory.SetBgValues(ref bgWidth, ref bgHeight, ref cellWidth, ref cellHeight, ref cellPadding);

        SetTabPositions(playerInventory.transform);

        cellUIPos = new Vector3((-bgWidth / 2) + (cellWidth / 2) + cellPadding, (bgHeight / 2) - (cellHeight / 2) - cellPadding, 0);
        int cellIndex = 0;
        for (int x = 0; x < playerInventory.cellRowAmount; x++)
        {
            for (int y = 0; y < playerInventory.cellCollumnAmount; y++)
            {
                Transform cell = playerInventory.cells[cellIndex].transform;
                cell.gameObject.SetActive(true);
                cell.localPosition = cellUIPos;
                cellUIPos.y -= cellHeight + cellPadding;
                cellIndex++;
            }
            cellUIPos.x += cellWidth + cellPadding;
            cellUIPos.y = (bgHeight / 2) - (cellHeight / 2) - cellPadding;
        }
    }

    public void OpenQuickBarUI()
    {
        CloseOtherUI();
        image.enabled = true;
        quickBarUIGO.SetActive(true);
    }

    public void OpenCraftingUI()
    {
        craftingUIOpen = true;
        CloseOtherUI();
        image.enabled = true;
        craftingUIGO.SetActive(true);
        craftingUI.SetUiSizeIncludingCells();
        craftingUI.SetBgValues(ref bgWidth, ref bgHeight, ref cellWidth, ref cellHeight, ref cellPadding);

        SetTabPositions(craftingUI.transform);

        cellUIPos = new Vector3((-bgWidth / 2) + (cellWidth / 2) + cellPadding, (bgHeight / 2) - (cellHeight / 2) - cellPadding, 0);
        int cellIndex = 0;
        for (int x = 0; x < GameController.Instance.craftableBuildings.Count; x++)
        {
            Transform cell = craftingUI.buildingCells[cellIndex].transform;
            cell.gameObject.SetActive(true);
            cell.localPosition = cellUIPos;
            cellUIPos.x += cellWidth + cellPadding;
            cellIndex++;
            if(cellIndex % craftingUI.cellCollumnAmount == 0)
            {
                cellUIPos.x = (-bgWidth / 2) + (cellWidth / 2) + cellPadding;
                cellUIPos.y -= cellHeight + cellPadding;
            }
            
        }
        cellIndex = 0;
        for (int i = 0; i < GameController.Instance.craftableItems.Count; i++)
        {
            Transform cell = craftingUI.craftableItemCells[cellIndex].transform;
            cell.gameObject.SetActive(true);
            cell.localPosition = cellUIPos;
            cellUIPos.x += cellWidth + cellPadding;
            cellIndex++;
            if (cellIndex % craftingUI.cellCollumnAmount == 0)
            {
                cellUIPos.x = (-bgWidth / 2) + (cellWidth / 2) + cellPadding;
                cellUIPos.y -= cellHeight + cellPadding;
            }
        }
    }

    public void OpenResearchUI()
    {
        researchUIOpen = true;
        CloseOtherUI();
        image.enabled = true;
        researchUIGO.SetActive(true);
        researchUI.SetUiSizeIncludingCells();
        researchUI.SetBgValues(ref bgWidth, ref bgHeight, ref cellWidth, ref cellHeight, ref cellPadding);

        SetTabPositions(researchUI.transform);

        float levelIndicatorWidth = researchUI.levelIndicatorWidth;

        cellUIPos = new Vector3((-bgWidth/2) + ((levelIndicatorWidth) + cellPadding), (bgHeight / 2) - (cellHeight / 2) - cellPadding, 0);

        for (int L = 1; L <= researchUI.maxResearchRequirementLevel; L++)// loop through all the research level tiers available in the game
        {
            researchUI.levelIndicators[L - 1].transform.localPosition = new Vector3(cellUIPos.x, (cellUIPos.y - ((cellHeight + cellPadding) * (L - 1))), 0);
            for (int i = 0; i < researchUI.researches.Length; i++)
            {
                ResearchNode node = researchUI.researchNodeCells[i].GetComponent<ResearchNode>();
                if (node.levelToUnlock == L)
                {
                    tempXNodeList.Add(node);
                }
            }

            for (int x = 0; x < tempXNodeList.Count; x++)
            {
                Transform cell = tempXNodeList[x].transform;// get a reference to a new cell
                ResearchNode node = cell.GetComponent<ResearchNode>();

                cell.gameObject.SetActive(true);// Activate the node object

                float tempYPos = cellUIPos.y;// set a local temp reference to the y position
                float tempXPos = cellUIPos.x;
                tempYPos -= ((cellHeight + cellPadding) * (L - 1));// set the y position based on the level of the research requirement
                tempXPos += ((((cellWidth / 2) + (levelIndicatorWidth / 2) + cellPadding)) + ((cellWidth + cellPadding) * x));
                Vector3 newPos = new Vector3(tempXPos, tempYPos, 0);
                cell.localPosition = newPos;
                
            }
            tempXNodeList.Clear();

        }
    }

    public void CloseResearchUI()
    {
        researchUIOpen = false;
        researchUIGO.SetActive(false);
        image.enabled = false;
    }

    public void CloseCraftingUI()
    {
        craftingUIOpen = false;
        craftingUIGO.SetActive(false);
        image.enabled = false;
    }

    public void CloseBuildingUI()
    {
        inventoryUIOpen = false;
        buildingUIGO.SetActive(false);
        image.enabled = false;
    }

    public void ClosePlayerInventoryUI()
    {
        playerInventory.DisableCells();
        //playerInventoryUIGO.SetActive(false);
        inventoryUIOpen = false;
        image.enabled = false;
    }

    public void CloseQuickBarUI()
    {
        quickBarUIGO.SetActive(false);
    }
    #endregion

    public void ToggleCraftingUI()
    {
        if(craftingUIOpen)
        {
            craftingUIOpen = false;
            CloseCraftingUI();
        }
        else
        {
            craftingUIOpen = true;
            OpenCraftingUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventoryUIOpen)
        {
            inventoryUIOpen = false;
            ClosePlayerInventoryUI();
        }
        else
        {
            inventoryUIOpen = true;
            OpenPlayerInventoryUI();
        }
    }

    public void ToggleResearchUI()
    {
        if (researchUIOpen)
        {
            researchUIOpen = false;
            CloseResearchUI();
        }
        else
        {
            researchUIOpen = true;
            OpenResearchUI();
        }
    }
}
