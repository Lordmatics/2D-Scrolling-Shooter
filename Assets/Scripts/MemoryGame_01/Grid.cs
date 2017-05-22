using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CustomAsset/MemoryGame/Grid", order = 1)]
public class Grid : ScriptableObject
{
    public int rows;
    public int columns;
}
