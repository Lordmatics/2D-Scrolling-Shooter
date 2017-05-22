using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionText : MonoBehaviour {

    private Text resText;    
	// Use this for initialization
	void Start ()
    {
        resText = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        resText.text = "Resolution : " + Utility.GetGameResWidth().ToString() + " x " + Utility.GetGameResHeight().ToString();
	}
}
