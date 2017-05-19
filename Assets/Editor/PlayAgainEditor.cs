using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayAgain))]
public class PlayAgainEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script simply, resets the game when pressed", MessageType.Info);

        base.OnInspectorGUI();
    }
}
