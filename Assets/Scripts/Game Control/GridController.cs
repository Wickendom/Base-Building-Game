using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    public static GridController Instance;

    public Grid grid;

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

        grid = GetComponent<Grid>();
    }

    public Vector3 ReturnGridPosition(Vector3 inputPosition)
    {
        Vector3 placementPos = grid.WorldToCell(inputPosition);
        return placementPos;
    }
}
