using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUICell : MonoBehaviour {

    public BuildingData buildingData;

    public void CreateBuilding()
    {
        UIController.Instance.CloseCraftingUI();

        BuildingPlacement.Instance.CreateBuildingLayout(buildingData);
    }
}
