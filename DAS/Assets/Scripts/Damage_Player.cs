using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Damage_Player : MonoBehaviour
{
    public static int health = 4;
    public int nbDamage = 1;
    public GameObject psDestroyPrefab;
    public float pauseTime = 1f;

    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    private int fullhealth;

    private void Start()
    {
        health = 4;
        fullhealth = health;
    }

    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            GameObject psDestroy = Instantiate(psDestroyPrefab, transform.position, transform.rotation);
            Destroy(psDestroy, 1f);
            if (pauseTime <= 0f)
            {
                Time.timeScale = 0f;
            }
            else
            {
                pauseTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Enemy Normal Bullet")
        {
            health -= nbDamage;
            OnPlayerDamage();
        }
    }

    void OnPlayerDamage()
    {
        for (int i = health; i < fullhealth; i++)
        {
            if (health != fullhealth)
            {
                hearts[i].sprite = emptyHeart;
            }
        }
        for (int i = 0; i < health; i++)
        {
            if (health != 0)
            {
                hearts[i].sprite = fullHeart;
            }
            
        }
    }
}
