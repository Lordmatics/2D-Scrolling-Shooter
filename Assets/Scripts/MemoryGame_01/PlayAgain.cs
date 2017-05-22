using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/PlayAgainButton")]
public class PlayAgain : MonoBehaviour
{

    private Button playAgainButton;

    void Start ()
    {
        playAgainButton = GetComponent<Button>();	
	}

    void Update()
    {
        playAgainButton.interactable = Difficulty.instance.HasSettings();
    }

    public void Pressed()
    {
        Score.instance.ResetScore();
        TileOutcome.instance.ResetText();
        //ColourStash.instance.ModifyMaterialArray()
        // Also runs CreateGrid on pressed
    }
}
