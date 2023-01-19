using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI highscore;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void OnEnable()
    {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString() + " Kills";
    }
}
