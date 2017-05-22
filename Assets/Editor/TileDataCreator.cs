using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileDataCreator : MonoBehaviour
{

    [MenuItem("NiallsWindow/Tile")]
    public static void Setup()
    {
        TileImage asset = ScriptableObject.CreateInstance<TileImage>();

        SerializedObject info = new SerializedObject(asset);

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/TileImageData.asset");

        //asset.tileSprite = info.FindProperty("rows").;

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;

        EditorUtility.SetDirty(asset);
    }
}
