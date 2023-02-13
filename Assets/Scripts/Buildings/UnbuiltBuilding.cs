using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UnbuiltBuilding : MonoBehaviour {

    public BuildingData buildingData;

    public string uniqueBuildingID;

    public GameObject resourceRequirementUIGo;

    public UnbuiltBuildingReasouceUI[] resourceRequirementUIComponents;
    public LocalCopyOfResourceUIData localCopyOfResourceUIData;

    private void Start()
    {
        InitialiseBuilding(buildingData);
    }

    public void InitialiseBuilding(BuildingData data)
    {
        buildingData = data;
    }

    public void BuildingPlaced()
    {
        resourceRequirementUIComponents = new UnbuiltBuildingReasouceUI[buildingData.resorceAmount.Length];
        uniqueBuildingID = BuildingManager.Instance.AddNewUniqueID(this);
        CreateUI();
    }

    public void OnEnable()
    {
        SaveLists.SaveEvent += SaveBuildingData;
    }

    public void OnDisable()
    {
        SaveLists.SaveEvent -= SaveBuildingData;
    }

    void CreateUI()
    {
        Vector3 spawnPos = new Vector3(-0.4f, 0);
        
        for (int i = 0; i < buildingData.resourceType.Length; i++)
        {
            GameObject go = Instantiate(resourceRequirementUIGo, transform);
            go.transform.localPosition = spawnPos;
            spawnPos.x += 0.8f;// This offsets the next ui spawn by 0.8

            Text[] uiTexts = GetComponentsInChildren<Text>();
            uiTexts[0].text = buildingData.resourceType[i].itemName;
            uiTexts[1].text = buildingData.resorceAmount[i].ToString();

            UnbuiltBuildingReasouceUI resourceRequirementUI = go.GetComponent<UnbuiltBuildingReasouceUI>();
            resourceRequirementUI.item = buildingData.resourceType[i];
            resourceRequirementUI.requirementAmount = buildingData.resorceAmount[i];
            resourceRequirementUI.unbuiltBuilding = this;

            resourceRequirementUIComponents[i] = resourceRequirementUI;

            go.transform.SetParent(ScreenSpaceCanvas.Instance.transform);
        }
    }

    public void BuildBuilding()
    {
        GetComponent<SpriteRenderer>().sprite = buildingData.sprite;
        Building building = gameObject.AddComponent<Building>();
        building.buildingData = buildingData;
        Player.addExp(10);
        for (int i = 0; i < resourceRequirementUIComponents.Length; i++)
        {
            Destroy(resourceRequirementUIComponents[i].gameObject);
        }
        Destroy(this);
    }

    public bool CheckIfAllResourcesMet()
    {
        for (int i = 0; i < resourceRequirementUIComponents.Length; i++)
        {
            if(resourceRequirementUIComponents[i].requirementMet == false)
            {
                return false;//if one of the buildings' resource requirements aren't met return false.
            }
        }

        return true;
    }

    public void SaveBuildingData(object sender, EventArgs args)
    {
        SavedUnbuiltBuilding savedUnbuiltBuilding = new SavedUnbuiltBuilding();

        savedUnbuiltBuilding.buildingID = buildingData.ID;
        savedUnbuiltBuilding.posX = transform.position.x;
        savedUnbuiltBuilding.posY = transform.position.y;

        for (int i = 0; i < resourceRequirementUIComponents.Length; i++)
        {
            savedUnbuiltBuilding.itemsNeededToCraft.Add(resourceRequirementUIComponents[i].item.ID);
            savedUnbuiltBuilding.amountOfItemsNeeded.Add(resourceRequirementUIComponents[i].requirementAmount);
        }

        SaveLists.Instance.savedBuildingList.savedUnbuiltBuildings.Add(savedUnbuiltBuilding);
    }
}

public class LocalCopyOfResourceUIData
{
    public ItemData item;
    public int requirementAmount;
}
