using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Building {

    public GeneratorUILayout UILayout;
    public LocalGeneratorUIData UIHolder;

    public GeneratorBuildingData generatorBuildingData;

    public InventoryCell outputCell;

    public bool UIOpen;

	// Use this for initialization
	public override void Start () {
        UIHolder = new LocalGeneratorUIData();
        generatorBuildingData = (GeneratorBuildingData)buildingData;
        Initialise();
	}

    public override void Initialise()
    {
        buildingData.Initialise(gameObject);
        gameObject.tag = "BuildingWithUI";
        buildingID = buildingData.ID;
        print(gameObject.name + " called Initialise func");
        uniqueBuildingID = BuildingManager.Instance.AddNewUniqueID(this);

        UIHolder.outputItem = null;
        UIHolder.outputItemStack = 0;

        StartCoroutine("Generate");
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Generate()
    {
        while(true)
        {
            yield return new WaitForSeconds(generatorBuildingData.timeToGenerateItem);
            if (UIHolder.outputItemStack < generatorBuildingData.generatedResource.maxStackAmount)
            {
                print("Resource Generated");
                UIHolder.outputItem = generatorBuildingData.generatedResource;
                UIHolder.outputItemStack++;
                if (UIOpen)
                {
                    outputCell.AddItem(generatorBuildingData.generatedResource);
                }
            }
            
        }
    }

    public override void OpenBuildingUI()
    {
        UIOpen = true;
        UIController.Instance.OpenBuildingUI(this, UILayout);
    }

    public void GetLocalItems(BuildingUI buildingUI)
    {
        ItemData[] items = new ItemData[1];

        items[0] = UIHolder.outputItem;

        int[] itemStackAmounts = new int[1];

        itemStackAmounts[0] = UIHolder.outputItemStack;

        buildingUI.SetInventoryCellsItems(items, itemStackAmounts);
    }
}

public struct LocalGeneratorUIData
{
    public ItemData outputItem;
    public int outputItemStack;
}
