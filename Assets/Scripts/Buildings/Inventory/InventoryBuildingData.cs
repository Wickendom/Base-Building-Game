using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Inventory Building")]
public class InventoryBuildingData : BuildingData {

    public override void Initialise(GameObject obj)
    {
        InventoryBuilding inventoryBuilding = obj.GetComponent<InventoryBuilding>();
        inventoryBuilding.buildingName = buildingName;
        inventoryBuilding.sprite = sprite;
        inventoryBuilding.buildingWidth = buildingWidth;
        inventoryBuilding.buildingHeight = buildingHeight;
        inventoryBuilding.UILayout = (InventoryBuildingUILayout)buildingUILayout;
    }
}
