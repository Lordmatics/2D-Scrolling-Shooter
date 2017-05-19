using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridDimension))]
public class Dimension : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script is used in the Difficulty Selection", MessageType.Info);

        base.OnInspectorGUI();
    }
}
