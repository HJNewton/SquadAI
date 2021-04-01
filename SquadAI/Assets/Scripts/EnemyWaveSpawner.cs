using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string waveNumber;
        public Transform enemy;
        public int enemyCount;
        public float spawnRate;
    }

    [Header("Settings")]
    public Wave[] waves; // Array of Wave classes
    public Transform[] spawnPoints; // Array of spawn points
    private int nextWave = 0;

    public float timeBetweenWaves = 5f; // Time between each wave
    public float waveCountdown; // TIme until next wave begins

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive()) // If all enemies are dead
            {
                WaveCompleted();
            }

            else
            {
                return;
            }
        }

        if (waveCountdown <= 0) // If wave is ready to spawn
        {
            if (state != SpawnState.SPAWNING) // Not currently spawning enemies
            {
                StartCoroutine(SpawnWave(waves[nextWave])); // Start spawning wave
            }

        }

        else // If wave is not ready to spawn
        {
            waveCountdown -= Time.deltaTime; // Reduces value of wave countdown 
        }
    }

    void WaveCompleted() // What occurs when wave is completed
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) // If next wave is bigger than number of waves we have
        {
            Debug.Log("ALL WAVES COMPLETE");
            //ALLOW PLAYER TO MOVE ON
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null) // Are any enemies alive?
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING; // Is spawning

        for (int i = 0; i < wave.enemyCount; i++) // Loop through number of enemies
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        state = SpawnState.WAITING; // Waiting for all enemies to die

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, _sp.position, _sp.rotation);
    }
}
