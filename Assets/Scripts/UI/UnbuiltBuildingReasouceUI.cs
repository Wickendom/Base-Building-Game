using UnityEngine;
using UnityEngine.UI;

public class UnbuiltBuildingReasouceUI : MonoBehaviour {

    public ItemData item;
    public int requirementAmount;

    public UnbuiltBuilding unbuiltBuilding;

    public Text requirementAmountText;

    public bool requirementMet;

    private void Start()
    {
        requirementAmountText = GetComponentsInChildren<Text>()[1];
    }

    public void AddItemToBuilding()
    {
        if (!requirementMet)
        {
            if (InventoryControl.Instance.AddItemToUnbuiltBuilding(item))
            {
                requirementAmount--;
                requirementAmountText.text = requirementAmount.ToString();
                if(requirementAmount == 0)
                {
                    requirementMet = true;
                }

                if (unbuiltBuilding.CheckIfAllResourcesMet())
                {
                    unbuiltBuilding.BuildBuilding();
                }
            }
        }
        
    }
}
