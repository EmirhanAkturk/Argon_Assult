using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text scoreText;
    int score;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();    
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        //find the number of digits of the score 
        int numberOfDigits = (int)Math.Floor(Math.Log10(score) + 1);

        //Print it in 5 digits for visuality.
        string text = "";

        //Fill empty digits with 0
        for (int i = 0; i < (5 - numberOfDigits); ++i)
            text += '0';

        //add score text after zeros
        text += score.ToString();

        //Update score text
        scoreText.text = $"Score: {text}";
    }
}
