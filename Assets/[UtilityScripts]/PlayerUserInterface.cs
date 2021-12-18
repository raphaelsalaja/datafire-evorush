using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterface : MonoBehaviour
{
    private GameManager GameManager;
    private Text timeRemainingText;
    private Text scoreText;
    

    public void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        timeRemainingText = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    private void Update()
    {
        float time = GameManager.GetCurrentGameTime();

        if (time <= 0)
        {
            timeRemainingText.text = "TIMES UP !!";
            scoreText.text = "SCORE: " + GameManager.GetCurrentGameScore().ToString();
        }
        else
        {
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f2");
            timeRemainingText.text = "TIME: " + minutes + "." + seconds;
            scoreText.text = "SCORE: " + GameManager.GetCurrentGameScore().ToString();
        }
    }

}
