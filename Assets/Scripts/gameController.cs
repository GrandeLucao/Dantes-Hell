using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public Text healthText;
    public GameObject pauseObj;
    private bool isPaused;
    public GameObject gameOverObj;
    public static gameController instance;

    //Awake eh inicializado antes de todos os metodos start() do seu projeto
    void Awake()
    {
        instance=this;

    }


    void Update()
    {
        PauseGame();

    }

    public void UpdateLives(int value)
    {
        healthText.text="x "+value.ToString();

    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused=!isPaused;
            pauseObj.SetActive(isPaused);
        }

        if(isPaused)
        {
            Time.timeScale=0f;
        }
        else
        {
            Time.timeScale=1f;
        }
    }

    public void GameOver()
    {
        Time.timeScale=0f;
        gameOverObj.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Level()
    {
        SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(3);
    }
}
