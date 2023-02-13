using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchController : MonoBehaviour {

    [SerializeField]
    private List<ResearchNode> researchNodes;// a list of all research nodes
    public  List<ResearchNode> _researchNodes
    {
        get
        {
            return researchNodes;
        }
    }

    [SerializeField]
    private int researchPoints;// how many points the player can spen on research nodes

    public delegate void AddResearchPoint();
    public static AddResearchPoint addResearchPoint;

    public delegate void RemoveResearchPoint();
    public static RemoveResearchPoint removeResearchPoint;

    public delegate ResearchNode ReturnResearchNode(ResearchData researchData);
    public static ReturnResearchNode returnResearchNode;

    public delegate void AddResearchNode(ResearchNode researchNode);
    public static AddResearchNode addResearchNode;

    // Use this for initialization
    void Awake () {
        addResearchPoint = OnAddResearchPoint;
        addResearchNode = OnAddResearchNode;
        removeResearchPoint = OnRemoveResearchPoint;
        returnResearchNode = ReturnResearchNodeWithData;
        researchNodes = new List<ResearchNode>();
	}

    public void UnlockResearchNode(ResearchNode selectedNode)
    {
        if(!selectedNode.unlocked)
        {
            selectedNode.UnlockNode();
            removeResearchPoint();
        }
    }

    private void OnAddResearchPoint()
    {
        researchPoints++;
    }
    
    private void OnAddResearchNode(ResearchNode researchNode)
    {
        researchNodes.Add(researchNode);
    }

    private void OnRemoveResearchPoint()
    {
        researchPoints--;
    }

    private ResearchNode ReturnResearchNodeWithData(ResearchData researchData)
    {
        //print(gameObject.name + " is searching for " + researchData.name);
        for(int i = 0; i < researchNodes.Count; i++)
        {
            //print("iterating through nodes, this was found " + researchNodes[i].researchData.name);
            if (researchData == researchNodes[i].researchData)
            {
                //print("found " + researchNodes[i].researchData.name + " when looking for research Data");
                return researchNodes[i];
            }
        }
        //print("Unable to find Node with " + researchData + " in it");
        return null;
    }
}
