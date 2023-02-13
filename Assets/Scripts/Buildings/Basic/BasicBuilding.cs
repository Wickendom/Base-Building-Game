using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicBuilding : Building {

    public override void Initialise()
    {
        buildingData.Initialise(gameObject);
        buildingID = buildingData.ID;
        uniqueBuildingID = BuildingManager.Instance.AddNewUniqueID(this);
    }

    public void OnEnable()
    {
        SaveLists.SaveEvent += SaveBuildingData;
    }

    public void OnDisable()
    {
        SaveLists.SaveEvent -= SaveBuildingData;
    }

    public void SaveBuildingData(object sender, EventArgs args)
    {
        SavedBasicBuilding savedBasicBuilding = new SavedBasicBuilding();

        savedBasicBuilding.buildingID = buildingID;
        savedBasicBuilding.posX = transform.position.x;
        savedBasicBuilding.posY = transform.position.y;

        SaveLists.Instance.GetBuildingList().savedBasicBuildings.Add(savedBasicBuilding);
    }
}
