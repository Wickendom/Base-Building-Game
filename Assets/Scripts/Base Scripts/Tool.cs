using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : MonoBehaviour, IUseable {

    public string toolName;
    public Item item;
    public GridLayout gridLayout;
    public bool checkingTile;
    private RectTransform checkTileSprite;

    public void Start()
    {
        gridLayout = GameController.Instance.gridLayout;
        checkTileSprite = GameController.Instance.checkTileSprite.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(checkingTile)
        {
            CheckTile();
        }
    }

    public virtual void Use()
    {
        //Player.Instance.EquipTool(this);
        StartTool();
    }

    public void CheckTile()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 placementPos = gridLayout.WorldToCell(pos);
        checkTileSprite.transform.position = placementPos;

        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    public void StartTool()
    {
        checkingTile = !checkingTile;
    }
}
