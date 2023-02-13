using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingUI/Inventory UI Layout")]
public class InventoryBuildingUILayout : BuildingUILayout {

    public int inventoryCellAmount;
    public InventoryCell[] cells;
}
