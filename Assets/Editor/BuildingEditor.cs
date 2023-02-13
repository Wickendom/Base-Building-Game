using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{ 
    /*Building building;
    Editor buildingEditor;
    Editor uiLayoutEditor;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawSettingsEditor(building.buildingData, building.OnShapeSettingsUpdated, ref building.buildingDataFoldout, ref buildingEditor);
        //BuildingData buildingData = building.buildingData;
        //DrawLayoutEditor(buildingData.buildingUILayout, buildingData.OnUILayoutUpdated, ref buildingData.UILayoutfoldout, ref uiLayoutEditor);

        building.buildingData.buildingUILayout.SetLayout();
    }

    void DrawSettingsEditor(Object data, System.Action onDataChanged, ref bool foldout, ref Editor editor)
    {
        if (data != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, data);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(data, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onDataChanged != null)
                        {
                            onDataChanged();
                        }
                    }

                }
            }
        }
    }

    void DrawLayoutEditor(Object layoutData, System.Action onDataChanged, ref bool foldout, ref Editor editor)
    {
        if (layoutData != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, layoutData);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(layoutData, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onDataChanged != null)
                        {
                            onDataChanged();
                        }
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        building = (Building)target;
    }*/
}
