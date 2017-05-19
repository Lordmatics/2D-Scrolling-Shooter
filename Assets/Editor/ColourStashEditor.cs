using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColourStash))]
public class ColourStashEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script contains the fixed amount of colours for the generated tiles", MessageType.Info);

        base.OnInspectorGUI();
    }
}
