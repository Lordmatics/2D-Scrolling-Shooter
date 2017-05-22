using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/TileOutcome")]
public class TileOutcome : MonoBehaviour
{
    private Text tileOutcomeText;

    public static TileOutcome instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tileOutcomeText = GetComponent<Text>();
        tileOutcomeText.text = "Press Play";

        //ResetText();
    }

    public void UpdateText(int points, bool outcome = false)
    {
        switch(outcome)
        {
            case true:
                tileOutcomeText.text = "Correct +" + points.ToString();
                Invoke("ClearText", MemoryGame.delay);
                break;
            case false:
                tileOutcomeText.text = "Incorrect -" + points.ToString();
                Invoke("ClearText", MemoryGame.delay);
                break;
        }
    }

    void ClearText()
    {
        tileOutcomeText.text = "Make a match!";
    }

    public void EndOfGameText()
    {
        tileOutcomeText.text = "Play Again?";
    }

    public void ResetText()
    {
        tileOutcomeText.text = "Start Matching";
    }
}
