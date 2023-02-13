using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    public GridLayout gridLayout;

    public GameObject checkTileSprite;
    public RectTransform canvasRect;

    [SerializeField]
    public List<BuildingData> craftableBuildings;
    [SerializeField]
    public List<ItemData> craftableItems;

    [HideInInspector]
    public bool isSceneBeingLoaded = false;
    //[HideInInspector]
    //public bool isSceneBeingTransitioned = false; //This will need adding later when i need to change scenes.

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void UpdateCraftableBuildings(BuildingData newBuilding)
    {
        craftableBuildings.Add(newBuilding);
        UIController.Instance.UpdateCraftingUI(newBuilding);
    }

    public void UpdateCraftableItems(ItemData newItem)
    {
        craftableItems.Add(newItem);
        UIController.Instance.UpdateCraftingUI(newItem);
    }
}
