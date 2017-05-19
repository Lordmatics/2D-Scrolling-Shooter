using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BuildTile))]
public class BuiltTileEditor : Editor
{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This script will instantiate tiles", MessageType.Info);

        base.OnInspectorGUI();

        BuildTile script = (BuildTile)target;

        if(script != null)
        {
            if (GUILayout.Button("Build Tile"))
            {
                script.InstantiateTile();
            }
        }

    }
}
