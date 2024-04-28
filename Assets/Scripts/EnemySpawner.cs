using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public List<GameObject> enemyPrefabs;
        public List<string> enemyName;
        public List<int> enemyCount; //Numero de enemigos que queremos que spawneen en X oleada
        public int waveQuota; //Numero total de enemigos de la oleada
        public float spawnInterval; //Intervalo de spawn de enemigos
        public int spawnCount; //Numero de enemigos spawneados



    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //Listado de todas las oleadas del juego
    public int currentWaveCount;
    public GameObject spawnPoint;


    [Header("Spawner Attributes")]
    float spawnTimer; //Tiempo para determinar cuando spawnear el siguiente enemigo

    Transform player;
    public GameObject[] roomLimits;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CombatePJ>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        //Comprueba si hay que spawnear otro enemigo

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    void CalculateWaveQuota()
    {
        int CurrentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            CurrentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = CurrentWaveQuota;
        Debug.LogWarning(CurrentWaveQuota);
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Vector2 spawnPosition = spawnPoint.transform.position;

                    // Instancia el enemigo
                    GameObject enemyClone = Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    // Activa la visibilidad del enemigo clonado
                    enemyClone.SetActive(true);

                    // Incrementa el contador de spawn del grupo de enemigos y de la oleada
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
}
 