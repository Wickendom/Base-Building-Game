using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Resouce : MonoBehaviour {

    public string resourceName;

    [SerializeField]
    private ItemData ItemData;
    public ItemData itemData
    {
        get
        {
            return ItemData;
        }
    }

    // Use this for initialization
    void Start () {
        resourceName = ItemData.itemName;
	}
}
