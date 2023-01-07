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


    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(true);
        startButton.SetActive(true);

        scoreText.text = "";
        levelText.text = "";
        pointsText.text = "";
        levelTitleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {
        background.SetActive(false);
        startButton.SetActive(false);
    }
}
