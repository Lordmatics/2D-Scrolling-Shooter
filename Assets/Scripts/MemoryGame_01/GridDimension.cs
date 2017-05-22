using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/GridDimension")]
public class GridDimension : MonoBehaviour
{
    [SerializeField]
    private MyDimension dimension;


    public int gridIndex = 0;

    public Grid grid;

    [SerializeField]
    private Color selectedColor;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        grid = (Grid)Resources.Load("GridData" + gridIndex.ToString(), typeof(Grid));

        DetermineData();

        //SerializedObject obj = new SerializedObject(grid);

        //SerializedProperty serRows = obj.FindProperty("rows");
        //SerializedProperty serCols = obj.FindProperty("columns");
        //SerializedProperty serGridStart = obj.FindProperty("gridStart");

        //EditorUtility.SetDirty(grid);     
    }

    void DetermineData()
    {
        switch(dimension)
        {
            case MyDimension.E_Easy:
                grid.rows = 3;
                grid.columns = 4;
                break;
            case MyDimension.E_Medium:
                grid.rows = 4;
                grid.columns = 4;
                break;
            case MyDimension.E_Hard:
                grid.rows = 4;
                grid.columns = 6;
                break;
            case MyDimension.E_Insane:
                grid.rows = 6;
                grid.columns = 6;
                break;
        }
    }

    public void Pressed()
    {
        ColorBlock block = button.colors;
        block.normalColor = selectedColor;
        button.colors = block;
    }

    public void Unpressed()
    {
        ColorBlock block = button.colors;
        block.normalColor = Color.white;
        button.colors = block;
    }
}
