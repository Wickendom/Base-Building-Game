using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator  {

    Noise noise = new Noise();

    public float EvaluateNoise(Vector3 point)
    {
        float noiseValue = (noise.Evaluate(point));
        return noiseValue;
    }

    void CreateResource(GameObject resource, int xPos, int yPos, Transform oreParent)
    {
        GameObject newResource = MonoBehaviour.Instantiate(resource);
        newResource.transform.parent = oreParent;
        Vector3 pos = GridController.Instance.ReturnGridPosition(new Vector3(xPos, yPos + 1, 0));
        SpriteRenderer rend = newResource.GetComponent<SpriteRenderer>();
        pos.x += rend.bounds.size.x / 2;
        pos.y -= rend.bounds.size.y / 2;
        newResource.transform.position = pos;
    }

    public void StartGeneration(int floorWidth, int floorHeight,int floorPositionX, int floorPositionY, GameObject[] resources, float noiseScale, float noiseValueFalloff, List<Vector3> randomStartPoint, Transform oreParent)
    {
        for (int x = 0; x < floorWidth; x++)
        {
            for (int y = 0; y < floorHeight; y++)
            {
                bool resourcePlaced = false;
                for (int i = 0; i < resources.Length; i++)
                {
                    float value = EvaluateNoise((new Vector3((x + (floorPositionX * floorWidth)) * noiseScale, (y + (floorPositionY * floorHeight)) * noiseScale, 0)) + randomStartPoint[i]);

                    if (value > noiseValueFalloff && resourcePlaced !=true)
                    {
                        resourcePlaced = true;
                        CreateResource(resources[i], x + (floorPositionX * floorWidth), y + (floorPositionY * floorHeight), oreParent);
                    }
                }
            }
        }
    }

}
