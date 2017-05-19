using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/GridDimension")]
public class GridDimension : MonoBehaviour
{
    [SerializeField]
    private MyDimension dimension;

    
    public Grid grid = new Grid();

    [SerializeField]
    private Color selectedColor;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        DetermineData();
    }

    void DetermineData()
    {
        switch(dimension)
        {
            case MyDimension.E_Easy:
                grid.rows = 3;
                grid.columns = 4;
                grid.gridStart = new Vector3(-8.2f, 0.0f, 7.5f);
                break;
            case MyDimension.E_Medium:
                grid.rows = 4;
                grid.columns = 4;
                grid.gridStart = new Vector3(-8.25f, 0.0f, 10.0f);
                break;
            case MyDimension.E_Hard:
                grid.rows = 4;
                grid.columns = 6;
                grid.gridStart = new Vector3(-13.7f,0.0f,9.5f);
                break;
            case MyDimension.E_Insane:
                grid.rows = 6;
                grid.columns = 6;
                grid.gridStart = new Vector3(-14.5f,0.0f,14.5f);
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
