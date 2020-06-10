using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;
    public Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore()
    {
        score++;
        updateScore();
    }

    public void resetScore()
    {
        score = 0;
        updateScore();
    }

    public void updateScore()
    {
        txtScore.text = "Score: " + score;
    }
}
