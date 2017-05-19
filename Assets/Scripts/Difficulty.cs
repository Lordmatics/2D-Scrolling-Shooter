using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MyDimension { E_Easy, E_Medium, E_Hard, E_Insane }


[AddComponentMenu("Scripts/Game/Difficulty")]
public class Difficulty : MonoBehaviour
{
    
    public GridDimension currentSelectedDimension;

    public static Difficulty instance;

    void Awake()
    {
        instance = this;
    }

    public bool HasSettings()
    {
        return currentSelectedDimension;
    }

    public void UpdateSelection(GridDimension script)
    {
        foreach (GridDimension s in transform.GetComponentsInChildren<GridDimension>())
        {
            s.Unpressed();
        }
        currentSelectedDimension = script;
        currentSelectedDimension.Pressed();
    }

    public Grid GetGridSettings()
    {
        return currentSelectedDimension.grid;
    }
}
