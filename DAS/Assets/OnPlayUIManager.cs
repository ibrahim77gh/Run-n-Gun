using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class OnPlayUIManager : MonoBehaviour
{
    public TextMeshProUGUI kills;
    public static int killCount;
    public TextMeshProUGUI highScore;



    private void Start()
    {
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString() + " Kills";
        killCount = 0;
    }
    private void Update()
    {
        kills.text = "KILLS: " + killCount.ToString();
        if (killCount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", killCount);
            highScore.text = "High Score: " + killCount.ToString() + " Kills";
        }
    }

    //private void OnEnable()
    //{
    //    Damage_Player.OnPlayerDamage += PlayerDamage;
    //}
    //private void OnDisable()
    //{
    //    Damage_Player.OnPlayerDamage -= PlayerDamage;
    //}

    //void PlayerDamage()
    //{
    //    for (int i = Damage_Player.health; i < fullhealth; i++)
    //    {
    //        hearts[i].sprite = emptyHeart;
    //    }
    //    for (int i = 0; i < Damage_Player.health; i++)
    //    {
    //        hearts[i].sprite = fullHeart;
    //    }
    //}
}
