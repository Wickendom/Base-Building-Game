using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenBase : MonoBehaviour {

    public GameObject cellObject;
    public Transform canvas;
    //[HideInInspector]
    public RectTransform backgroundUI;

    public int cellRowAmount, cellCollumnAmount;// how many cells can fit within the width/ height of the ui background.
    [HideInInspector]
    public float bgWidth, bgHeight;
    public float cellWidth, cellHeight;
    public float cellPadding;

    public virtual void Start()
    {
        backgroundUI = UIController.Instance.UIBackground;
    }

    public void SetUiSizeIncludingCells()
    {
        bgWidth = (cellRowAmount * cellWidth) + ((cellRowAmount + 1) * cellPadding);
        bgHeight = (cellCollumnAmount * cellHeight) + ((cellCollumnAmount + 1) * cellPadding);
        backgroundUI.sizeDelta = new Vector2(bgWidth, bgHeight);
    }

    public void SetBaseUISize(float width, float height)
    {
        bgWidth = width;
        bgHeight = height;
        backgroundUI.sizeDelta = new Vector2(width, height);
    }

    public void SetBgValues(ref float BGWidth, ref float BGHeigt, ref float CellWidth, ref float CellHeight, ref float CellPadding)
    {
        BGWidth = bgWidth;
        BGHeigt = bgHeight;
        CellWidth = cellWidth;
        CellHeight = cellHeight;
        CellPadding = cellPadding;
    }
}
