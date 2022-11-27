using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    [Header("Player")]
    public GameObject player;
    public HealthSystem healthSystem;
    public bool inCombat;
    [Header("Enemies")]
    public Transform[] spawnPoints;
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    [Header("Waves")]
    public SpawnState state;
    public float timeBetweenWaves = 5f;
    public float waveTimeCountdown;
    private Coroutine activeCoroutine;
    public Wave[] waves;
    public int nextWave = 0;


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.transform.name == player.transform.name)
            inCombat = true;
    }

    private void Awake()
    {
        waveTimeCountdown = timeBetweenWaves;
        state = SpawnState.COUNTING;
    }

    private void CurrentlyInCombat()
    {
        // Check if time that passed from the beginning of the OnTriggerEnter
        if (waveTimeCountdown <= 0)
        {
            waveTimeCountdown = timeBetweenWaves;

            // If time is less than 0 we need to check if we are spawning something at the moment
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                activeCoroutine = StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        // if not, decrese countdown
        else
        {
            if (state != SpawnState.SPAWNING && state != SpawnState.WAITING)
                waveTimeCountdown -= Time.deltaTime;
        }

        // Check if current state is WAITING
        if (state != SpawnState.WAITING && state != SpawnState.SPAWNING)
            return;

        // Check if enemies are still alive
        if (EnemyIsAlive())
            return;
     
        // Begin a new round
        WaveCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is still alive
        if (!healthSystem.IsStillAlive())
        {
            // End players life
            healthSystem.Die();

            // Stop the coroutine
            EndCoroutine();

            // Remove all enemies from the scene
            GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemiesToDestroy.Length; i++)
                Destroy(enemiesToDestroy[i]);

            // Restore all basic variables for player etc
            inCombat = false;
            nextWave = 0;
            healthSystem.RestoreFullHealth();
            player.transform.position = healthSystem.lastCheckpoint;
            //player.GetComponent<Rigidbody>().position = healthSystem.lastCheckpoint.transform.position;
            //player.transform.rotation = healthSystem.lastCheckpoint.transform.rotation;
            waveTimeCountdown = timeBetweenWaves;
            state = SpawnState.COUNTING;
        }
        
        if (inCombat)
        {
            CurrentlyInCombat();
            return;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        // Change state to COUNTING
        state = SpawnState.COUNTING;
        waveTimeCountdown = timeBetweenWaves;

        // We need to check if there are more waves
        if (nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("All waves complete... Looping!");
            nextWave = 0;
            inCombat = false;
        }
        // If there are more waves...
        else
            nextWave++;
    }

    bool EnemyIsAlive()
    {
        GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");

        //Debug.Log(enemiesToDestroy.Length);

        if (enemiesToDestroy.Length != 0)
            return true;

        // TODO: this one might create some bugs, but we will deal with it later
        return false;
    }
    
    IEnumerator SpawnWave(Wave _wave)
    {
        // Change the current status to SPAWNING
        state = SpawnState.SPAWNING;

        // Debug which wave we are spawning
        Debug.Log("Spawning Wave: " + _wave.name);

        // Spawn as much enemies as we need
        for (int i = 0; i < _wave.count; i++)
        {
            // TODO: check this one - range is exlusive in the max
            SpawnEnemies(_wave.enemies[Random.Range(0, _wave.enemies.Length)]);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        
        // After we spawn enemies, we're changing it to WAITING state
        state = SpawnState.WAITING;

        // End the current coroutine to prevent errors
        EndCoroutine();
    }

    private void EndCoroutine()
    {
        StopCoroutine(activeCoroutine);
    }

    void SpawnEnemies(Transform _enemies)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemies, _sp.position, transform.rotation);
    }
}
