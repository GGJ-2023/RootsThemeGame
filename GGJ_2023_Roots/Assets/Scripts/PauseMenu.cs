using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    public static PauseMenu instance;
    public float loadingDelay = .01f;
    public float time;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        */
    }
    public void Pause()
    {
        if(GameManager.instance != null) { 

            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    IEnumerator MenuFix()
    {
        pauseMenuUI.SetActive(true);
        yield return new WaitForSeconds(.5f);        
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    public void Quit()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void MainMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Game()
    {
        GameIsPaused = false;
        time += Time.deltaTime;
        if (time > loadingDelay)
        {
            SceneManager.LoadScene("SampleScene");
        }
        
    }
}
