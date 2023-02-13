using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingUI/Crafter UI Layout")]
public class CrafterUILayout : BuildingUILayout {

    public InventoryCell inputCell;
    public Vector3 inputCellPos;

    public InventoryCell outputCell;
    public Vector3 outputCellPos;

    public InventoryCell fuelCell;
    public Vector3 fuelCellPos;
}
