using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomMaterial
{
    public string name = "";
    public MaterialCheck customMaterial;
    public int useCount = 2;

    public CustomMaterial()
    {
        name = "";
        useCount = 2;
    }

    public bool ConsumeMaterial()
    {
        useCount--;
        bool bIsConsumed = useCount <= 0 ? true : false;
        return bIsConsumed;
    }
}

[System.Serializable]
public class MaterialCheck
{
    public Material customMat;
    public string tileDataString = "Apple";
    public TileImage tileImageData;
    public bool bIsValid = true;

    void Start()
    {
        tileImageData = (TileImage)Resources.Load("TileData_"+ tileDataString, typeof(TileImage));
    }

    public MaterialCheck(bool _check = true)
    {
        bIsValid = _check;
    }
}

[AddComponentMenu("Scripts/Game/ColourStash")]
public class ColourStash : MonoBehaviour
{
    [Header("Stash of colours")]
    public List<CustomMaterial> materials = new List<CustomMaterial>();

    [SerializeField]
    private List<CustomMaterial> copyMaterials = new List<CustomMaterial>();

    public static ColourStash instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            CustomMaterial temp = materials[i];
            copyMaterials.Add(temp);
        }
        RefreshUseCount();
    }

    void RefreshUseCount()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].useCount = 2;
        }
        for (int i = 0; i < copyMaterials.Count; i++)
        {
            copyMaterials[i].useCount = 2;
        }
    }

    public void RefillMaterialArray()
    {
        for (int i = 0; i < copyMaterials.Count; i++)
        {
            materials.Add(copyMaterials[i]);
        }
        RefreshUseCount();
    }

    public void ModifyMaterialArray(Difficulty script)
    {
        int max = (int)script.currentSelectedDimension.grid.rows * (int)script.currentSelectedDimension.grid.columns;
        max /= 2;
        if (materials.Count > max)
        {
            //Modify Array to suit difficulty
            for (int i = materials.Count - 1; i >= max; i--)
            {
                materials.RemoveAt(i);
                //Debug.Log(materials.Count + "MATCOUNT");
                //Debug.Log(max + "MAX");
            }
        }
    }

    public MaterialCheck GetRandomMaterial()
    {
        // TODO What happens when u have no materials left
        if (materials.Count <= 0) return new MaterialCheck(false);

        // Pick a random colour
        int randIndex = Random.Range(0, materials.Count);



        bool bConsumed = materials[randIndex].ConsumeMaterial();
        // Check if material has been assigned its maximum amount of times
        if(bConsumed)
        {
            // Since this is the last assignment, remove it from the material array
            MaterialCheck temp = materials[randIndex].customMaterial;
            materials.Remove(materials[randIndex]);
            return temp;
        }
        else
        {
            return materials[randIndex].customMaterial;
        }
    }
}
