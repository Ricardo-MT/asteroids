using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool isLooping = true;
    [SerializeField] float nextWaveDelay = 2f;
    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currentWave;

    MyEventManager eventManager;

    void Start()
    {
        StartCoroutine(RunWaves());
        eventManager = FindObjectOfType<MyEventManager>();
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator RunWaves()
    {
        do

        {
            // Loop through all the waves
            // and spawn enemies for each wave
            // with a delay of nextWaveDelay
            for (int i = 0; i < waveConfigs.Count; i++)
            {
                currentWave = waveConfigs[i];
                StartCoroutine(SpawnEnemies());
                yield return new WaitForSeconds(nextWaveDelay);
            }
        }
        while (isLooping);
    }

    IEnumerator SpawnEnemies()
    {
        // Spawn enemies at the starting waypoint
        // using the prefab from the wave config
        // and the number of enemies specified in the wave config
        // with a rate of spawnRate
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            var enemy = Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.identity, this.transform);
            var enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.OnEnemyDestroyed += () => eventManager.Trigger(new EnemyDestroyedEvent(enemyScript));
            yield return new WaitForSeconds(currentWave.GetSpawnRate());
        }
    }
}