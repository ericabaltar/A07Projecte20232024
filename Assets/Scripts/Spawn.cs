using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public int numberOfEnemiesPerWave = 5;
    public float timeBetweenWaves = 10f;
    public int numberOfWaves = 3;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnWaves());
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
            // Selecciona aleatoriamente uno de los puntos de spawn
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instancia el enemigo en la posición del punto de spawn previamente seleccionado aleatoriamente
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

