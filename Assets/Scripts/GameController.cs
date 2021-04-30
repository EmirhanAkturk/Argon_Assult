using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject informationPanel;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text healthText;

    [Header("Master Timeline")]
    [SerializeField] PlayableDirector masterTimeline;

    // Start is called before the first frame update
    void Start()
    {
        if (Global.IsFirstPlay) {
            Global.IsFirstPlay = false;
            Time.timeScale = 0;
            informationPanel.SetActive(true);
            scoreText.enabled = false;
            healthText.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            informationPanel.SetActive(false);
            scoreText.enabled = true;
            healthText.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeyPress();
        
    }

    private void KeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            informationPanel.SetActive(false);
            scoreText.enabled = true;
            healthText.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
