using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    ResourceGenerator resourceGenerator;
    public float noiseScale;
    [Range(0,1)]
    public float noiseValueFalloff;
    public Transform floorParent;
    public Transform oreParent;
    public GameObject floorObject;
    public int FloorsToCreate;
    public float xPadding;
    public float yPadding;
    public GameObject[] resources;
    List<Vector3> resourceSeeds;

    // Use this for initialization
    void Start()
    {
        resourceGenerator = new ResourceGenerator();

        resourceSeeds = new List<Vector3>();

        for (int i = 0; i < resources.Length; i++)
        {
            resourceSeeds.Add(new Vector3(UnityEngine.Random.Range(-100000, 100000), (UnityEngine.Random.Range(-100000, 100000)), 0));
        }

        Vector3 spawnPos = Vector3.zero;
        MeshRenderer floorRend = floorObject.GetComponent<MeshRenderer>();
        float floorWidth = floorRend.bounds.size.x;
        float floorHeight = floorRend.bounds.size.y;
        spawnPos.y += floorRend.bounds.size.y / 2;
        float offsetX = floorRend.bounds.size.x;
        float offsetY = floorRend.bounds.size.y;

        for (int x = 0; x < FloorsToCreate; x++)
        {
            spawnPos.x = (x==0) ? floorWidth / 2: (offsetX  * x) + (floorWidth / 2);
            for (int y = 0; y < FloorsToCreate; y++)
            {
                spawnPos.y = (y == 0) ? floorHeight / 2 : (offsetY  * y) + (floorWidth / 2);
                GameObject newFloor = Instantiate(floorObject,floorParent);

                MeshRenderer rend = newFloor.GetComponent<MeshRenderer>();
                
                newFloor.transform.position = spawnPos;

                rend.material.SetVector("_offset", new Vector2(-x, -y));

                resourceGenerator.StartGeneration(6,6,x,y, resources, noiseScale, noiseValueFalloff,resourceSeeds, oreParent);
            }

            spawnPos.y = 0;
        }
    }

    public void OnEnable()
    {
        SaveLists.SaveEvent += SaveWorldData;
    }

    public void OnDisable()
    {
        SaveLists.SaveEvent -= SaveWorldData;
    }

    public void SaveWorldData(object sender, EventArgs args)
    {
        SavedWorldData savedWorldData = new SavedWorldData();
        savedWorldData.resourceSeeds = resourceSeeds;

        SaveLists.Instance.savedWorldData = savedWorldData;
    }
}
