using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNode : MonoBehaviour {

    public ResearchData researchData;
    public bool unlocked = false;
    public List<ResearchNode> requiredNodesToUnlock;//this is a list of nodes that need to be unlocked before you can unlock this one
    public List<ResearchNode> otherNodesNeededToUnlock;//this is a list of nodes that this node is a requirement of to unlock others.
    public BuildingData buildingToUnlock;
    public int levelToUnlock;
    private Image image;
    private Text text;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    public void Initialise()
    {
        gameObject.name = researchData.name;
        text.text = researchData.name;
        requiredNodesToUnlock = new List<ResearchNode>();
        otherNodesNeededToUnlock = new List<ResearchNode>();
        buildingToUnlock = researchData.buildingUnlock;
        levelToUnlock = researchData.levelToUnlock;
        AddRequiredResearchNodes();
        AddChildNodesToUnlock();
        
    }

    public void AddRequiredResearchNodes()
    {
        if(researchData.requiredNodesToUnlock.Length > 0)
        {
            for (int i = 0; i < researchData.requiredNodesToUnlock.Length; i++)
            {
                requiredNodesToUnlock.Add(ResearchController.returnResearchNode(researchData.requiredNodesToUnlock[i]));
            }
        }  
    }

    public void AddChildNodesToUnlock()
    {
        if (researchData.otherNodesNeededToUnlock.Length > 0)
        {
            for (int i = 0; i < researchData.otherNodesNeededToUnlock.Length; i++)
            {
                //print(gameObject.name + " is trying to add " + researchData.otherNodesNeededToUnlock[0].name + " to Other nodes list");
                otherNodesNeededToUnlock.Add(ResearchController.returnResearchNode(researchData.otherNodesNeededToUnlock[i]));
            }
        }
    }

    private void UpdateConnectedChildNodes()
    {
        for (int i = 0; i < otherNodesNeededToUnlock.Count; i++)
        {
            otherNodesNeededToUnlock[i].CheckRequiredNodes();
        }
    }

    public void UnlockNode()
    {
        if(Player.Instance.playerLevel >= levelToUnlock)
        {
            image.color = Color.gray;
            unlocked = true;
            GameController.Instance.UpdateCraftableBuildings(buildingToUnlock);
            UpdateConnectedChildNodes();
        }
    }

    private bool CheckRequiredNodes()
    {
        for (int i = 0; i < requiredNodesToUnlock.Count; i++)
        {
            if(requiredNodesToUnlock[i].unlocked == false)
            {
                return false;
            }
        }
        return true;
    }
}
