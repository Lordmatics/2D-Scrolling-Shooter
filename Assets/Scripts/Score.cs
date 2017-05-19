using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Game/Score")]
public class Score : MonoBehaviour
{

    private Text scoreText;
    public int scoreValue;

    public static Score instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score : " + scoreValue.ToString("0000");
    }

    public void ModifyScore(int value)
    {
        scoreValue += value;
        scoreValue = Mathf.Clamp(scoreValue, 0, 9999);
        UpdateScore();
    }

    public void ResetScore()
    {
        ModifyScore(-scoreValue);
    }
}
