using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TMP_Text newScoreText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        newScoreText.enabled = false;
        healthText.enabled = false;
        
        AssignTexts();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReloadLevel();

        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();            
    }

    private void AssignTexts()
    {
        //Assing lose panel score text
        scoreText.text = $"Your {newScoreText.text}";

        //Assing lose panel highest score text
        int highestScore = PlayerPrefs.GetInt("highestScore");

        if (Global.Score > highestScore)
        {
            PlayerPrefs.SetInt("highestScore", Global.Score);
        }

        string newScore = ScoreToString(highestScore);

        highestScoreText.text = $"Highest Score: {newScore}";
    }

    private string ScoreToString(int score)
    {
        //find the number of digits of the score 
        int numberOfDigits = (int)Math.Floor(Math.Log10(Global.Score) + 1);

        //Print it in 5 digits for visuality.
        string text = "";

        //Fill empty digits with 0
        for (int i = 0; i < (5 - numberOfDigits); ++i)
            text += '0';

        //add score text after zeros
        text += score.ToString();

        return text;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
