using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MemoryGame))]
public class MemoryGameEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script controls the game", MessageType.Info);

        base.OnInspectorGUI();
    }
}
