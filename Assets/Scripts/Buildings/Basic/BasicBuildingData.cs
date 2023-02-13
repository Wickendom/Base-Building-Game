using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Basic Building")]
public class BasicBuildingData : BuildingData {

    public override void Initialise(GameObject obj)
    {
        BasicBuilding basicBuilding = obj.GetComponent<BasicBuilding>();
        basicBuilding.buildingName = buildingName;
        basicBuilding.sprite = sprite;
        basicBuilding.buildingWidth = buildingWidth;
        basicBuilding.buildingHeight = buildingHeight;
    }
}
