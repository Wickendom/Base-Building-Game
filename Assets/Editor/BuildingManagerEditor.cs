using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildingManager))]
public class BuildingManagerEditor : Editor {

    Editor itemManagerEditor;
    BuildingManager buildingManager;
    SerializedProperty buildingDictionary;
    SerializedProperty unbuiltBuildingResourceUIGO;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.PropertyField(unbuiltBuildingResourceUIGO, new GUIContent("Unbuilt Building Resource UI Game Object"));

        if (GUILayout.Button("Add Building"))
        {
            buildingManager.AddBuildingSlot();
        }

        EditorGUILayout.Foldout(true, new GUIContent("List of Buildings"));
        EditorGUILayout.LabelField("ID");

        for (int i = 0; i < buildingDictionary.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(buildingDictionary.GetArrayElementAtIndex(i), new GUIContent(i.ToString()));
            if (GUILayout.Button("Remove Building"))
            {
                buildingManager.RemoveBuilding(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        buildingManager = (BuildingManager)target;

        buildingDictionary = serializedObject.FindProperty("fullBuildingList");
        unbuiltBuildingResourceUIGO = serializedObject.FindProperty("unbuitBuildingResourceUIGO");
    }
}
