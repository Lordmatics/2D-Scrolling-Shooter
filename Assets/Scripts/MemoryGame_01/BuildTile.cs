using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Game/BuildTile")]
public class BuildTile : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnPosition;

    public static BuildTile instance;

    private List<GameObject> activeTiles;
    
    void Awake()
    {
        instance = this;
    }

    public GameObject InstantiateTileAt(Vector3 pos, bool bUseCanvas = false)
    {
        if(activeTiles.Count <= 0 || activeTiles == null)
            activeTiles = new List<GameObject>();
        GameObject mainCanvas = GameObject.FindGameObjectWithTag("GameTiles");
        GameObject prefab = (GameObject)Instantiate(Resources.Load("TilePrefab_UI", typeof(GameObject)));
        if(prefab != null)
        {
            if(bUseCanvas)
                prefab.transform.SetParent(mainCanvas.transform);
            prefab.transform.position = pos;

            activeTiles.Add(prefab);
            return prefab;
        }
        return new GameObject();
    }

    public void InstantiateTile(bool bUseCanvas = false)
    {
        if (activeTiles.Count <= 0 || activeTiles == null)
            activeTiles = new List<GameObject>();
        GameObject mainCanvas = GameObject.FindGameObjectWithTag("GameTiles");
        GameObject prefab = (GameObject)Instantiate(Resources.Load("TilePrefab_UI", typeof(GameObject)));
        if(prefab != null)
        {
            if (bUseCanvas)
                prefab.transform.SetParent(mainCanvas.transform);
            prefab.transform.position = spawnPosition;
            activeTiles.Add(prefab);

        }
    }

    public void RemoveAllTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            GameObject temp = activeTiles[i];
            activeTiles.Remove(temp);
            Destroy(temp);
        }
    }
}
