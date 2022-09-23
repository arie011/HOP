using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName;
    public void Level1()
    {
        SceneManager.LoadScene("Loading");
        PlayerPrefs.SetString("sceneName", "Gameplay");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Loading");
        PlayerPrefs.SetString("sceneName", "Gameplay2");
    }
     public void Level3()
    {
        SceneManager.LoadScene("Loading");
        PlayerPrefs.SetString("sceneName", "GameplayLevel3");
    }
    public void ExitButton()
    {
        Debug.Log("QUIT THE GAME");
        Application.Quit();
    }

    public void OpenScene(string sceneToOpen)
    {
        SceneManager.LoadScene(sceneToOpen);
    }
}
