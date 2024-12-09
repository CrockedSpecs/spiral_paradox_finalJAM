using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject topLeft;
    public GameObject topLeftPortal;
    public List<GameObject> topLeftSpawner;
    public GameObject topRight;
    public GameObject topRightPortal;
    public List<GameObject> topRightSpawner;
    public GameObject bottomLeft;
    public GameObject bottomLeftPortal;
    public List<GameObject> bottomLeftSpawner;
    public GameObject bottomRight;
    public GameObject bottomRightPortal;
    public List<GameObject> bottomRightSpawner;

    public GameObject enemyPrefab; // Prefab del enemigo a instanciar
    public float spawnInterval = 2f; // Intervalo entre spawns regulares

    private int spawnMin = 1; // Número inicial mínimo de enemigos
    private int spawnMax = 3; // Número inicial máximo de enemigos
    private bool isExtraSpawning = false; // Indica si SpawnExtraEnemies está activo
    private bool isLevelFinished = false; // Indica si el nivel ya ha terminado

    void Start()
    {
        FindClosestPortals(); // Buscar los portales más cercanos al iniciar
        StartCoroutine(SpawnEnemies());
        StartCoroutine(UpdateSpawnLimits());
        StartCoroutine(SpawnExtraEnemies());
        StartCoroutine(CheckAllPortalsDestroyed());
    }

    void FindClosestPortals()
    {
        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");

        topLeftPortal = FindClosestPortal(topLeft, allSpawns);
        topRightPortal = FindClosestPortal(topRight, allSpawns);
        bottomLeftPortal = FindClosestPortal(bottomLeft, allSpawns);
        bottomRightPortal = FindClosestPortal(bottomRight, allSpawns);

        Debug.Log("Portales más cercanos asignados correctamente.");
    }

    GameObject FindClosestPortal(GameObject referencePoint, GameObject[] allSpawns)
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject spawn in allSpawns)
        {
            float distance = Vector3.Distance(referencePoint.transform.position, spawn.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = spawn;
            }
        }

        return closest;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isLevelFinished) yield break;

            // Si SpawnExtraEnemies está activo, espera
            if (isExtraSpawning)
            {
                yield return null;
                continue;
            }

            int enemiesToSpawn = Random.Range(spawnMin, spawnMax + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator UpdateSpawnLimits()
    {
        while (true)
        {
            if (isLevelFinished) yield break;

            yield return new WaitForSeconds(30f); // Incrementar cada 1/2 minuto
            spawnMin++;
            spawnMax++;
        }
    }

    IEnumerator SpawnExtraEnemies()
    {
        yield return new WaitForSeconds(120f);

        while (true)
        {
            if (isLevelFinished) yield break;

            isExtraSpawning = true;

            for (int i = 0; i < 6; i++)
            {
                SpawnEnemy();
            }

            isExtraSpawning = false;
            yield return new WaitForSeconds(60f);
        }
    }

    IEnumerator CheckAllPortalsDestroyed()
    {
        while (!isLevelFinished)
        {
            if (topLeftPortal == null && topRightPortal == null && bottomLeftPortal == null && bottomRightPortal == null)
            {
                isLevelFinished = true;
                FinishLevel();
            }

            yield return new WaitForSeconds(1f); // Revisa cada segundo
        }
    }

    void SpawnEnemy()
    {
        List<GameObject> selectedSpawnerList = GetRandomSpawnerList();
        if (selectedSpawnerList == null || selectedSpawnerList.Count == 0)
        {
            Debug.LogWarning("No hay spawners disponibles para esta zona.");
            return;
        }

        int attempts = 0;
        const int maxAttempts = 10; // Evitar bucles infinitos en caso de problemas

        while (attempts < maxAttempts)
        {
            GameObject randomSpawner = selectedSpawnerList[Random.Range(0, selectedSpawnerList.Count)];

            // Usar la posición actual del spawner y validar si está navegable
            Vector3 currentPosition = randomSpawner.transform.position;
            if (NavMesh.SamplePosition(currentPosition, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
            {
                // Instanciar enemigo en la posición válida del NavMesh
                GameObject enemy = EnemyPool.Instance.requestEnemy();
                enemy.transform.position = hit.position; // Posición validada
                enemy.transform.rotation = Quaternion.identity; // Ajustar rotación si es necesario
                Debug.Log($"Enemigo instanciado en {randomSpawner.name} ({hit.position})");
                return; // Salir al instanciar exitosamente
            }

            attempts++;
        }

        Debug.LogWarning("No se encontró una posición válida para instanciar un enemigo después de múltiples intentos.");
    }





    List<GameObject> GetRandomSpawnerList()
    {
        int randomZone = Random.Range(0, 4);

        switch (randomZone)
        {
            case 0: return topLeftPortal != null ? topLeftSpawner : null;
            case 1: return topRightPortal != null ? topRightSpawner : null;
            case 2: return bottomLeftPortal != null ? bottomLeftSpawner : null;
            case 3: return bottomRightPortal != null ? bottomRightSpawner : null;
            default: return null;
        }
    }

    void FinishLevel()
    {
        Debug.Log("¡Todos los portales han sido destruidos! Nivel terminado.");
        // Aquí puedes implementar cualquier lógica adicional para el fin del nivel
    }
}
