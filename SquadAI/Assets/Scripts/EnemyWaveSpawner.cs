using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public static EnemyWaveSpawner instance = null;

    public enum SpawnState 
    { 
        Spawning, 
        Waiting, 
        BetweenWaves, 
    };

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
    public GameObject nextWaveButton;

    private int nextWave = 0;
    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.BetweenWaves;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
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
    }

    public void StartNextWave()
    {
        StartCoroutine(SpawnWave(waves[nextWave])); // Start spawning wave
        nextWaveButton.SetActive(false);
    }

    void WaveCompleted() // What occurs when wave is completed
    {
        state = SpawnState.BetweenWaves;

        nextWaveButton.SetActive(true);

        if (nextWave + 1 > waves.Length - 1) // If next wave is bigger than number of waves we have
        {
            GameManagerScript.instance.gameState = GameManagerScript.GameState.GameWon;
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
        state = SpawnState.Spawning; // Is spawning

        for (int i = 0; i < wave.enemyCount; i++) // Loop through number of enemies
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        state = SpawnState.Waiting; // Waiting for all enemies to die

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, sp.position, sp.rotation);
    }
}
