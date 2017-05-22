using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTiles : MonoBehaviour
{
    public float width, height;

    private RectTransform myRect;

    private GridLayoutGroup gridLayoutGroup;

    public float widthDiviser, heightDiviser;
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    void Update()
    {
        width = myRect.rect.width;
        height = myRect.rect.height;
        Vector2 newSize = new Vector2(width / widthDiviser, height / heightDiviser);
        gridLayoutGroup.cellSize = newSize;
    }
}
