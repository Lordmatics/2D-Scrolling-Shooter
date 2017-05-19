using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileOutcome))]
public class TileOutcomeEditor : Editor
{


    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script displays the outcome of the matchup", MessageType.Info);

        base.OnInspectorGUI();
    }

}
