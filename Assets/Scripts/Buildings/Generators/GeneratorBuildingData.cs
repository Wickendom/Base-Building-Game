using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Generator Building")]
public class GeneratorBuildingData : BuildingData {

    public ItemData resourceRequiredToGenerate;
    public ItemData generatedResource;
    public int timeToGenerateItem;

    public override void Initialise(GameObject obj)
    {
        Generator generatorBuilding = obj.GetComponent<Generator>();
        generatorBuilding.buildingName = buildingName;
        generatorBuilding.sprite = sprite;
        generatorBuilding.buildingWidth = buildingWidth;
        generatorBuilding.buildingHeight = buildingHeight;
        generatorBuilding.UILayout = (GeneratorUILayout)buildingUILayout;
    }
}
