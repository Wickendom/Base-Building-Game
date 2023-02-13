using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemManager))]
public class ItemManagerEditor : Editor {

    Editor itemManagerEditor;
    ItemManager itemManager;
    SerializedProperty itemList;
    SerializedProperty defaultItem;
    
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.PropertyField(defaultItem, new GUIContent("Default Item"));
        if(GUILayout.Button("Add Item"))
        {
            itemManager.AddItemSlot();
        }

        EditorGUILayout.Foldout(true,new GUIContent("List of Items"));
        EditorGUILayout.LabelField("ID");

        for (int i = 0; i < itemList.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(itemList.GetArrayElementAtIndex(i), new GUIContent(i.ToString()));
            if(GUILayout.Button("Remove Item"))
            {
                itemManager.RemoveItem(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        itemManager = (ItemManager)target;

        itemList = serializedObject.FindProperty("fullItemList");
        defaultItem = serializedObject.FindProperty("defaultItem");
    }
}
