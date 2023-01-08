using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject background;
    public GameObject startButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI levelTitleText;

    public TextMeshProUGUI endWinText;
    public TextMeshProUGUI endTimeText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI endPushedText;
    public GameObject playAgainButton;

    public WriteJsonFile jsonCon;



    void Start() // At start only background and start button are displayed to player
    {
        background.SetActive(true);
        startButton.SetActive(true);

        scoreText.gameObject.SetActive(true);
        scoreText.text = "";
        levelText.gameObject.SetActive(true);
        levelText.text = "";
        pointsText.gameObject.SetActive(true);
        pointsText.text = "";
        levelTitleText.gameObject.SetActive(true);
        levelTitleText.text = "";

        endWinText.gameObject.SetActive(true);
        endWinText.text = "";
        endTimeText.gameObject.SetActive(true);
        endTimeText.text = "";
        endScoreText.gameObject.SetActive(true);
        endScoreText.text = "";
        endPushedText.gameObject.SetActive(true);
        endPushedText.text = "";
        playAgainButton.SetActive(false);
    }

    public void StartGame() // Background and start button are hidden when game starts
    {
        background.SetActive(false);
        startButton.SetActive(false);

    }

    public void PlayAgain() // End screen elements are hidden when the user selects to play again
    {
        endWinText.text = "";
        endTimeText.text = "";
        endScoreText.text = "";
        endPushedText.text = "";
        playAgainButton.SetActive(false);
    }

    public void EndScreen(float time, float score, int numPushed, bool win) // End screen is displayed to user
    {
        scoreText.text = "";
        levelText.text = "";
        pointsText.text = "";
        levelTitleText.text = "";
        playAgainButton.SetActive(true);

        if (win)
        {
            endWinText.text = "Winner";
            jsonCon.writeJSON(time, score, numPushed);
        }
        else
        {
            endWinText.text = "Loser";
        }

        endTimeText.text = "Time: " + time;
        endScoreText.text = "Score: " + score;
        endPushedText.text = "Pushed Objects: " + numPushed;

        
    }
}
