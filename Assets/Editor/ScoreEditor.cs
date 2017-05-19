using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Score))]
public class ScoreEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script tracks user score", MessageType.Info);

        base.OnInspectorGUI();
    }
}
