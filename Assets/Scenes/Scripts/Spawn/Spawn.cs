using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript1 : MonoBehaviour
{
    public int numberOfEnemiesPerWave = 5;
    public float timeBetweenWaves = 10f;
    public int numberOfWaves = 3;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public UnityEvent setActive;

    private bool spawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartSpawning();
            Debug.Log("Empiezan las oleadas");
        }
    }

    void StartSpawning()
    {
        if (!spawning)
        {
            spawning = true;
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < numberOfEnemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

