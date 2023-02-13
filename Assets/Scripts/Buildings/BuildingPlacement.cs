using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public static BuildingPlacement Instance; 

    public GridLayout gridLayout;
    public GameObject placementSquare;

    public GameObject unbuiltBuilding;

    private bool placingBuilding;
    private GameObject currentObject;
    public BuildingData currentBuildingData;
    private Bounds currentObjectBounds;
    public GameObject[] placementTileArray;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {

        if (placingBuilding)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 placementPos = gridLayout.WorldToCell(pos);
            currentObject.transform.position = placementPos;
            
            placementPos += new Vector3(currentObjectBounds.size.x / 2, currentObjectBounds.size.y / 2);

            if(Input.GetMouseButtonDown(0))
            {
                
                Color alphaTemp = currentObject.GetComponent<SpriteRenderer>().color;
                alphaTemp.a = 1;
                currentObject.GetComponent<SpriteRenderer>().color = alphaTemp;
                
                for (int i = 0; i < placementTileArray.Length; i++)
                {
                    Destroy(placementTileArray[i]);
                }

                placingBuilding = false;
                currentObject.GetComponent<UnbuiltBuilding>().BuildingPlaced();
                currentObject = null;
            }
        }
    }

    public void CreateBuildingLayout(BuildingData building)
    {
        GameObject go = Instantiate(unbuiltBuilding);
        currentObject = go;
        currentBuildingData = building;
        go.GetComponent<UnbuiltBuilding>().buildingData = building;

        placementTileArray = new GameObject[building.buildingWidth * building.buildingHeight];
        int arrayIncrement = 0;
        for (int x = 0; x < building.buildingWidth; x++)
        {
            for (int y = 0; y < building.buildingHeight; y++)
            {
                GameObject temp = Instantiate(placementSquare, go.transform);
                Bounds bounds = temp.GetComponent<Collider2D>().bounds;
                temp.transform.localPosition = new Vector3(bounds.size.x * x, bounds.size.y * y);
                temp.transform.localPosition -= new Vector3(bounds.size.x / 2, bounds.size.y / 2);
                placementTileArray[arrayIncrement] = temp;
                arrayIncrement++;
            }
        }
        go.AddComponent<BoxCollider2D>();
        currentObjectBounds = currentObject.GetComponent<Collider2D>().bounds;

        Color alphatemp = go.GetComponent<SpriteRenderer>().color;
        alphatemp.a = 0.7f;
        go.GetComponent<SpriteRenderer>().color = alphatemp;

        placingBuilding = true;
    }
}
