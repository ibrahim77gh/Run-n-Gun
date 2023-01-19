using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject OnPlayMenu;
    public float timeDelay = 1f;
    public GameObject destroyEffect;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        OnPlayMenu.SetActive(true);
        gameOverPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Damage_Player.health <= 0 && player != null)
        {
            deathByEnemy();
        }
    }
    void deathByEnemy()
    {
        Instantiate(destroyEffect, player.transform.position, player.transform.rotation);
        // FindObjectOfType<Audio_Manager>().Play("Explosion");
        Invoke("GameOver", timeDelay);
        Destroy(player);
        //Destroy(GameObject.FindGameObjectWithTag("Spawner"));
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        OnPlayMenu.SetActive(false);
    }
}