using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    private int spawnMin = 1; // N�mero inicial m�nimo de enemigos
    private int spawnMax = 3; // N�mero inicial m�ximo de enemigos
    private bool isExtraSpawning = false; // Indica si SpawnExtraEnemies est� activo
    private bool isLevelFinished = false; // Indica si el nivel ya ha terminado

    void Start()
    {
        FindClosestPortals(); // Buscar los portales m�s cercanos al iniciar
        StartCoroutine(SpawnEnemies());
        StartCoroutine(UpdateSpawnLimits());
        StartCoroutine(SpawnExtraEnemies());
        StartCoroutine(CheckAllPortalsDestroyed());
    }

    private void Awake()
    {
        FindClosestPortals(); // Buscar los portales m�s cercanos al iniciar
    }

    void FindClosestPortals()
    {
        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");

        topLeftPortal = FindClosestPortal(topLeft, allSpawns);
        topRightPortal = FindClosestPortal(topRight, allSpawns);
        bottomLeftPortal = FindClosestPortal(bottomLeft, allSpawns);
        bottomRightPortal = FindClosestPortal(bottomRight, allSpawns);

        Debug.Log("Portales m�s cercanos asignados correctamente.");
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

            // Si SpawnExtraEnemies est� activo, espera
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
            yield return new WaitForSeconds(1f); // Revisa cada segundo
            if ((topLeftPortal == null || !topLeftPortal.activeInHierarchy) &&
                (topRightPortal == null || !topRightPortal.activeInHierarchy) &&
                (bottomLeftPortal == null || !bottomLeftPortal.activeInHierarchy) &&
                (bottomRightPortal == null || !bottomRightPortal.activeInHierarchy))
            {
                isLevelFinished = true;
                FinishLevel();
            }
        }
    }

    void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
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

        // Lista de �ndices para iterar por todos los spawners
        List<int> spawnerIndices = new List<int>();
        for (int i = 0; i < selectedSpawnerList.Count; i++)
        {
            spawnerIndices.Add(i);
        }

        // Mezclar los �ndices para seleccionar spawners aleatoriamente
        ShuffleList(spawnerIndices);

        foreach (int index in spawnerIndices)
        {
            GameObject spawner = selectedSpawnerList[index];

            if (spawner == null || !spawner.activeInHierarchy)
            {
                continue; // Ignorar spawners desactivados o nulos
            }

            // Obtener la posici�n actual del spawner
            Vector3 currentPosition = spawner.transform.position;

            // Validar si la posici�n es navegable
            if (NavMesh.SamplePosition(currentPosition, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
            {
                // Instanciar el enemigo en una posici�n v�lida
                GameObject enemy = EnemyPool.Instance.requestEnemy();
                enemy.transform.position = hit.position;
                enemy.transform.rotation = Quaternion.identity;
                Debug.Log($"Enemigo instanciado en posici�n v�lida cerca de {spawner.name}");
                return;
            }
        }

        // Si no se encuentra una posici�n v�lida, buscar fuera de la lista seleccionada
        Debug.LogWarning("No se encontr� una posici�n v�lida en la lista seleccionada. Buscando otra posici�n v�lida...");

        GameObject[] allSpawners = GameObject.FindGameObjectsWithTag("EnemySpawn");
        foreach (GameObject spawner in allSpawners)
        {
            if (spawner == null || !spawner.activeInHierarchy)
            {
                continue;
            }

            Vector3 currentPosition = spawner.transform.position;

            if (NavMesh.SamplePosition(currentPosition, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
            {
                GameObject enemy = EnemyPool.Instance.requestEnemy();
                enemy.transform.position = hit.position;
                enemy.transform.rotation = Quaternion.identity;
                Debug.Log($"Enemigo instanciado fuera de la lista inicial, en posici�n v�lida cerca de {spawner.name}");
                return;
            }
        }

        Debug.LogWarning("No se encontr� ninguna posici�n v�lida dentro o fuera de la lista seleccionada.");
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
        
        Debug.Log("�Todos los portales han sido destruidos! Nivel terminado.");
        // Aqu� puedes implementar cualquier l�gica adicional para el fin del nivel
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene(2);
        }
        else if (SceneManager.GetActiveScene().name == "Level12")
        {
            SceneManager.LoadScene(3);
        }
        else if (SceneManager.GetActiveScene().name == "Level13")
        {
            SceneManager.LoadScene(4);
        }
    }
}
