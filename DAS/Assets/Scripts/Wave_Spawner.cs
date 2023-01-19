using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Wave_Spawner : MonoBehaviour
{
    public enum spawnState { spawning, waiting, counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int enemyCount;
        public float rate;
    }

    public Transform playerTrans;
    public Wave[] waves;
    public Transform[] spawnPoints;
    private int nextWave = 0;
    public float timeBetweenWaves = 3f;
    public float waveCountdown;
    private float searchCountdown = 1f;
    public spawnState state = spawnState.counting;

    public GameObject gameFinishedPanel;
    public GameObject OnPlayMenu;


    // Start is called before the first frame update
    void Start()
    {
        nextWave = 0;
        waveCountdown = timeBetweenWaves;
        gameFinishedPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCountdown <= 0 & state == spawnState.counting)
        {
            StartCoroutine(spawnWave(waves[nextWave]));
        }
        if (state == spawnState.waiting)
        {
            if (EnemyIsAlive() == false)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = spawnState.counting;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            GameOver();
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator spawnWave(Wave _wave)
    {
        state = spawnState.spawning;
        
        for (int i = 0; i < _wave.enemyCount; i++)
        {
            spawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        
        state = spawnState.waiting;
        yield break;
    }

    void spawnEnemy(GameObject _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameFinishedPanel.SetActive(true);
        OnPlayMenu.SetActive(false);
    }
}
