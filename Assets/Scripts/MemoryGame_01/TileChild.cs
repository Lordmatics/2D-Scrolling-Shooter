using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChild : MonoBehaviour {

    RectTransform myTransform;
    RectTransform parentRect;
    void Start()
    {
        myTransform = GetComponent<RectTransform>(); // 
        parentRect = transform.parent.GetComponent<RectTransform>();
        if (MemoryGame.bUseColour)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        myTransform = parentRect;
    }
}
