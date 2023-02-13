using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInventory))]
public class QuickbarController : MonoBehaviour {

    public static QuickbarController Instance;

    private PlayerInventory quickBar;

    [SerializeField]
    private InventoryCell currentlySelectedCell;
    [SerializeField]
    private ItemData selectedItemData;

    [SerializeField]
    private int selectedCellIndex;

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

    public void Initialise()
    {
        quickBar = GetComponent<PlayerInventory>();
        currentlySelectedCell = quickBar.cells[0];
        selectedItemData = currentlySelectedCell.item;
        selectedCellIndex = 0;
        currentlySelectedCell.GetComponent<Button>().Select();
    }

    public ItemData ReturnSelectedItemData()
    {
        return selectedItemData;
    }

    public void UpdateSelectedCell(int index)
    {
        
        selectedCellIndex += index;
        if(selectedCellIndex >= quickBar.cells.Count)
        {
            selectedCellIndex = 0;
        }
        if(selectedCellIndex <= -1)
        {
            selectedCellIndex = quickBar.cells.Count - 1;
        }
        currentlySelectedCell = quickBar.cells[selectedCellIndex];
        selectedItemData = currentlySelectedCell.item;
        currentlySelectedCell.GetComponent<Button>().Select();
    }
	
	// Update is called once per frame
	void Update () {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0 && !Input.GetKey(KeyCode.LeftControl))
        {
            UpdateSelectedCell((int)Mathf.Sign(-scrollWheel));
        }
	}
}
