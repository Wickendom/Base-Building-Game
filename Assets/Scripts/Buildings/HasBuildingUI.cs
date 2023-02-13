using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HasBuildingUI {

    public abstract List<InventoryCell> inventoryCells();
    public abstract void OpenBuildingUI();
}
