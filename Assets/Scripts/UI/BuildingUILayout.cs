using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUILayout : ScriptableObject {

    public GameObject inventoryCell;
    public Sprite bgSprite;
    public int bgWidth;
    public int bgHeight;
    public float cellPadding;

    public int cellAmount;
    public int amountOfInputSlots;
    public int amountOfOutputSlots;
}
