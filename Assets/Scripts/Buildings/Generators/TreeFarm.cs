using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFarm : Generator {

	// Use this for initialization
	public override void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator GenerateResource()
    {
        yield return new WaitForSeconds(generatorBuildingData.timeToGenerateItem);
        if (UIHolder.outputItem != null) UIHolder.outputItem = generatorBuildingData.generatedResource;
        UIHolder.outputItemStack++;
        outputCell.AddItem(generatorBuildingData.generatedResource);
    }

}
