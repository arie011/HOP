using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;

    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}
