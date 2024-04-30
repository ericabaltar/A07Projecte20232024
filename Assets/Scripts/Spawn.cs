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

    void Start()
    {
        //if (!spawning) return;
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

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Renderer enemyRenderer = enemy.GetComponent<Renderer>();
                if (enemyRenderer != null)
                {
                    enemyRenderer.enabled = true;
                }
                else
                {
                    Debug.LogWarning("El objeto con la etiqueta 'Enemy' no tiene un componente Renderer para controlar la visibilidad.");
                }

            }
        }

    }

    void KilledAllEnemies()
    {
        setActive.Invoke();
    }
}

