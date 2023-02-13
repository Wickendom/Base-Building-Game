using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ResearchData : ScriptableObject {

    public BuildingData buildingUnlock; //This is the building that will get unlocked
    public int levelToUnlock; //The level the player needs to be before they can unlock this node
    public bool unlocked = false;
    public ResearchData[] requiredNodesToUnlock;//this is a list of nodes that need to be unlocked before you can unlock this one
    public ResearchData[] otherNodesNeededToUnlock;//this is a list of nodes that this node is a requirement of to unlock others.
}
