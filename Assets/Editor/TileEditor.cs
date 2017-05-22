using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script contains the tile data", MessageType.Info);

        base.OnInspectorGUI();

        Tile script = (Tile)target;
        if(GUILayout.Button("LiveUpdate Scale"))
        {
            if(script)
            {
                script.UpdateHoverScale();
            }
        }
    }
}
