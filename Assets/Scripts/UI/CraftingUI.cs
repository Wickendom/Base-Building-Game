using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : UIScreenBase {

    [SerializeField]
    GameObject craftableItemCellGO;

    [SerializeField]
    public List<BuildingUICell> buildingCells;
    [SerializeField]
    public List<CraftableItemUICell> craftableItemCells;

    public override void Start()
    {
        base.Start();
        CreateBuildingList();
        CreateCraftableItemList();
    }

    public void CreateBuildingList()
    {
        buildingCells = new List<BuildingUICell>();
        List<BuildingData> knownBuildings = GameController.Instance.craftableBuildings;

        for (int y = 0; y < knownBuildings.Count; y++)
        {
            GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, transform);
            RectTransform rectTemp = cell.GetComponent<RectTransform>();
            rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);
            BuildingUICell cellData = cell.GetComponent<BuildingUICell>();
            cellData.buildingData = knownBuildings[y];
            cellData.GetComponentInChildren<Text>().text = cellData.buildingData.buildingName;
            buildingCells.Add(cellData);
            cell.SetActive(false);
        }
    }

    public void CreateCraftableItemList()
    {
        craftableItemCells = new List<CraftableItemUICell>();
        List<ItemData> knownItems = GameController.Instance.craftableItems;

        for (int i = 0; i < knownItems.Count; i++)
        {
            GameObject cell = Instantiate(craftableItemCellGO, transform.position, Quaternion.identity, transform);
            RectTransform rectTemp = cell.GetComponent<RectTransform>();
            rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);
            CraftableItemUICell cellData = cell.GetComponent<CraftableItemUICell>();
            cellData.itemData = knownItems[i];
            cellData.GetComponentInChildren<Text>().text = cellData.itemData.itemName;
            craftableItemCells.Add(cellData);
            cell.SetActive(false);
        }
    }

    public void UpdateBuildingList(BuildingData buildingData)
    {
        GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, transform);
        RectTransform rectTemp = cell.GetComponent<RectTransform>();
        rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);
        BuildingUICell cellData = cell.GetComponent<BuildingUICell>();
        cellData.buildingData = buildingData;
        cellData.GetComponentInChildren<Text>().text = cellData.buildingData.buildingName;
        buildingCells.Add(cellData);
        cell.SetActive(false);
    }

    public void UpdateItemList(ItemData itemData)
    {
        GameObject cell = Instantiate(cellObject, transform.position, Quaternion.identity, transform);
        RectTransform rectTemp = cell.GetComponent<RectTransform>();
        rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);
        CraftableItemUICell cellData = cell.GetComponent<CraftableItemUICell>();
        cellData.itemData = itemData;
        cellData.GetComponentInChildren<Text>().text = cellData.itemData.itemName;
        craftableItemCells.Add(cellData);
        cell.SetActive(false);
    }
}
