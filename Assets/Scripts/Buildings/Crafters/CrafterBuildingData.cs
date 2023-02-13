using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Building/Crafter Building")]
public class CrafterBuildingData : BuildingData {

    private SimpleCrafter crafterBuilding;
    public ItemData[] itemsBuildingIsAbleToRefine;
    public bool requiresFuel;
    public ItemData[] fuelItems;

    public override void Initialise(GameObject obj)
    {
        SimpleCrafter crafterBuilding = obj.GetComponent<SimpleCrafter>();
        crafterBuilding.buildingName = buildingName;
        crafterBuilding.sprite = sprite;
        crafterBuilding.buildingWidth = buildingWidth;
        crafterBuilding.buildingHeight = buildingHeight;
        crafterBuilding.UILayout = (CrafterUILayout)buildingUILayout;
    }
}
