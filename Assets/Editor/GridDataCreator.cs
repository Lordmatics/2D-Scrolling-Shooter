using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridDataCreator
{

    public static int count = 0;

    [MenuItem("NiallsWindow/Grid")]
    public static void Setup()
    {
        Grid asset = ScriptableObject.CreateInstance<Grid>();

        SerializedObject info = new SerializedObject(asset);

        AssetDatabase.CreateAsset(asset, "Assets/Resources/GridData" + count.ToString() + ".asset");
        count++;

        asset.rows = info.FindProperty("rows").intValue;
        asset.columns = info.FindProperty("columns").intValue;

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;

        EditorUtility.SetDirty(asset);
    }
}
