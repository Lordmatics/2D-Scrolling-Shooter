using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/MemoryGame/BackButton")]
public class BackButton : MonoBehaviour
{

    public void ReturnToMenu()
    {
        // Rest is done on button

        // Delete Tiles
        BuildTile.instance.RemoveAllTiles();
        // Refill Materials Array - Colour Stash

        // ReEnable Settings window
    }
}
