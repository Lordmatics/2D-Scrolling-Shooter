using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Difficulty))]
public class DifficultyEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script will determine the grid offset and size", MessageType.Info);

        base.OnInspectorGUI();
    }
}
