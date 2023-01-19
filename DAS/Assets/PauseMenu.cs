using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pausePanel;
    // Update is called once per frame
    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
