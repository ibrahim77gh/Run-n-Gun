using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Enabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Wave_Spawner waveSpawner = GetComponent<Wave_Spawner>();
        waveSpawner.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Wave_Spawner waveSpawner = GetComponent<Wave_Spawner>();
            waveSpawner.enabled = true;
        }
    }
}
