using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameStatus gameStatus;
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    public bool isGameOver = false;
    
    float timer;
    public bool isDelayBetweenWave = false;
    public bool isTimerRunning = true;

    public GameObject pauseMenuScreen;
    public GameObject gameOverMenuScreen;

    public ScoreManager scoreManager;

    public WaveManager waveManager;
    public GameObject tutorialMenu;
    public GameObject winHeader, loseHeader;

    // Initialize Game
    void InitGame()
    {

    }

    private void Start()
    {
        winHeader.SetActive(false);
        loseHeader.SetActive(false);
    }
    private void Update()
    {
        ForTesting();
    }

    void ForTesting()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerCondition(bool win)
    {
        isGameOver = true;
        if (win)
        {
            Debug.Log("Player Win");
            winHeader.SetActive(true);
            gameOverMenuScreen.SetActive(true);
            //SistemSoal.instance.Gui_Soal.SetActive(false);

        }
        else
        {
            loseHeader.SetActive(true);
            Debug.Log("Player Lose");
            gameOverMenuScreen.SetActive(true);
            //SistemSoal.instance.Gui_Soal.SetActive(false);
        }
    }

    public void Pause()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        pauseMenuScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        tutorialMenu.SetActive(false);
        waveManager.Initialize();
        waveManager.gameRunning = true;
    }

}

public enum GameStatus
{
    Pause, FirstWave, SecondWave, ThirdWave, StopWave
}
