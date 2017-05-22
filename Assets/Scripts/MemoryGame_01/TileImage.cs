using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "CustomAsset/MemoryGame/TileImageData", order = 2)]
public class TileImage : ScriptableObject
{
    public Sprite tileSprite;
}
