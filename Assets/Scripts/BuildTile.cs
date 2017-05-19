using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Game/BuildTile")]
public class BuildTile : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnPosition;

    public static BuildTile instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject InstantiateTileAt(Vector3 pos)
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("TilePrefab", typeof(GameObject)));
        if(prefab != null)
        {
            prefab.transform.position = pos;
            return prefab;
        }
        return new GameObject();
    }

    public void InstantiateTile()
    {
        GameObject prefab = (GameObject)Instantiate(Resources.Load("TilePrefab", typeof(GameObject)));
        if(prefab != null)
        {
            prefab.transform.position = spawnPosition;
        }
    }
}
