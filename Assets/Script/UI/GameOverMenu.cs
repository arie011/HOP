using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    private ScoreManager scoreManager;
    public WaveManager waveManager;

    private int score = 0;
    private int maxScore = 0;
    public Text scoreDisplay;

    public Sprite[] gameOverStars;
    public Image[] starsImage;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameManager.Instance.scoreManager;
        CalculateMaxScore();
    }

    void CalculateMaxScore()
    {
        for (int i = 0; i < waveManager.waves.Length; i++)
        {
            maxScore += waveManager.waves[i].totalEnemyWave;
        }

        maxScore *= 100;

        StartCoroutine(CalculateScore());
    }

    IEnumerator CalculateScore()
    {
        while (score < scoreManager.score)
        {
            score += 10;
            scoreDisplay.text = score.ToString();
            yield return new WaitForSeconds(0.005f);
        }

        StartCoroutine(ShowingStars(score));
    }

    IEnumerator ShowingStars(int score)
    {
        var starsOpen = 0;

        var scorePersentage = (score / maxScore) * 100f;

        if (scorePersentage < 40f)
        {
            starsOpen = 1;
        }
        else if (scorePersentage > 40f && scorePersentage < 75f)
        {
            starsOpen = 2;
        }
        else
        {
            starsOpen = 3;
        }

        for (int i = 0; i < starsOpen; i++)
        {
            starsImage[i].sprite = gameOverStars[i];
            yield return new WaitForSeconds(0.3f);
        }
    }
}
