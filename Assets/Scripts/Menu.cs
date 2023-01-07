using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject background;
    public GameObject startButton;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(true);
        startButton.SetActive(true);

        scoreText.text = "";
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
