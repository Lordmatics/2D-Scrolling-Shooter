using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChild : MonoBehaviour {

    void Start()
    {
        if(MemoryGame.bUseColour)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
