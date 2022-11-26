using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private Coroutine coroutine;
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public bool inCombat;
    GameObject Player;
    GameObject[] enemiesToDestroy;


    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inCombat = true;
        }
    }
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<HealthSystem>().Restart)
        {
            // Zatrzymaj korutyne zwiazana z dana fala
            StopCoroutine(coroutine);

            // USUN WSZYSTKICH WROGOW ZE SCENY
            enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemiesToDestroy.Length; i++)
            {
                Destroy(enemiesToDestroy[i]);
            }

            // Przywroc wartosci poczatkowe parametrow
            inCombat = false;
            Player.GetComponent<HealthSystem>().Restart = false;
            Player.GetComponent<HealthSystem>().curentHealth = Player.GetComponent<HealthSystem>().maxHealth;
            waveCountdown = timeBetweenWaves;
            state = SpawnState.COUNTING;
        }

        // Spawn wave only when player enters the arena
        if (inCombat == true)
        {
            if (state == SpawnState.WAITING) // Wait to kill all enemies
            {
                // Check if enemies are still alive
                if (!EnemyIsAlive())
                {
                    // Begin a new round
                    WaveCompleted();
                }

                else
                {
                    return;
                }
            }
            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    // Start spawning wave
                    coroutine = StartCoroutine(SpawnWave(waves[nextWave])); 
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1 || Player.GetComponent<HealthSystem>().Restart)
        {
            nextWave = 0;
            Debug.Log("All waves complete... Looping!");
            inCombat = false;
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
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        Debug.Log("Spawning Wave: " + _wave.name);
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemies(_wave.enemies[Random.Range(0, _wave.enemies.Length)]);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        
        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemies(Transform _enemies)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemies, _sp.position, transform.rotation);
    }
}
