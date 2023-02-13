using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUI : UIScreenBase {

    ResearchController researchController;

    public ResearchData[] researches; // this is a complete list of all the things you can possibly research.
    public List<ResearchData> currentlyAvaliableResearch; // this is a list of the research you have unlocked as well as the next thing in the tree for anything you have unlocked.
    public List<GameObject> levelIndicators;

    [SerializeField]
    private GameObject researchNodeObject;

    public List<GameObject> researchNodeCells;
    public int maxResearchRequirementLevel = 0;
    public float levelIndicatorWidth;

    // Use this for initialization
    public override void Start () {
        base.Start();
        researchController = GameController.Instance.GetComponent<ResearchController>();
        levelIndicators = new List<GameObject>();
        CreateResearchUI();
        
    }
	
    void CreateResearchUI()
    {
        Vector3 NodePos = Vector3.zero;

        for (int i = 0; i < researches.Length; i++)
        {
            GameObject NodeGO = Instantiate(researchNodeObject, transform.position, Quaternion.identity, transform);
            RectTransform rectTemp = NodeGO.GetComponent<RectTransform>();
            rectTemp.sizeDelta = new Vector2(cellWidth, cellHeight);
            ResearchNode node = NodeGO.GetComponent<ResearchNode>();
            node.researchData = researches[i];
            ResearchController.addResearchNode(node);
            researchNodeCells.Add(NodeGO);
            
        }

        for (int i = 0; i < researches.Length; i++)
        {
            researchNodeCells[i].GetComponent<ResearchNode>().Initialise();
            researchNodeCells[i].SetActive(false);
        }

        

        for (int i = 0; i < researches.Length; i++)
        {
            if (researches[i].levelToUnlock > maxResearchRequirementLevel)
            {
                maxResearchRequirementLevel = researches[i].levelToUnlock;
            }
        }

        for (int i = 0; i < maxResearchRequirementLevel; i++)
        {
            GameObject temp = new GameObject();
            GameObject textGO = Instantiate(temp, transform.position, Quaternion.identity, transform);
            Text text = textGO.AddComponent<Text>();
            text.text = (i + 1).ToString();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.color = Color.black;
            text.fontSize = 52;
            text.alignment = TextAnchor.MiddleCenter;   
            textGO.name = ((i + 1).ToString() + (" Level Indicator"));
            levelIndicators.Add(textGO);
            levelIndicatorWidth = LayoutUtility.GetPreferredWidth(textGO.transform.GetComponent<RectTransform>());
        }

        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
