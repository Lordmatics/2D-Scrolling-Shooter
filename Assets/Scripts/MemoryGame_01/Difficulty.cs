using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MyDimension { E_Easy, E_Medium, E_Hard, E_Insane }


[AddComponentMenu("Scripts/Game/Difficulty")]
public class Difficulty : MonoBehaviour
{
    
    public GridDimension currentSelectedDimension;

    [SerializeField]
    private GridDimension[] gridDimensions;

    public static Difficulty instance;


    public Toggle toggle;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridDimensions = transform.parent.transform.GetComponentsInChildren<GridDimension>();
        toggle = GameObject.FindGameObjectWithTag("UseColourToggle").GetComponent<Toggle>();
    }

    public bool HasSettings()
    {
        return currentSelectedDimension;
    }

    public void UpdateSelection(GridDimension script)
    {
        foreach (GridDimension s in gridDimensions)
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

    void OnEnable()
    {
        foreach (GridDimension s in gridDimensions)
        {
            s.gameObject.SetActive(true);
        }
        if(toggle != null)
            toggle.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        foreach (GridDimension s in gridDimensions)
        {
            s.gameObject.SetActive(false);
        }
        if (toggle != null)
            toggle.gameObject.SetActive(false);

    }
}
