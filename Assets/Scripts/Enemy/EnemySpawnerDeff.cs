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

    private int spawnMin = 1; // Número inicial mínimo de enemigos
    private int spawnMax = 3; // Número inicial máximo de enemigos
    private bool isExtraSpawning = false; // Indica si SpawnExtraEnemies está activo
    private bool isLevelFinished = false; // Indica si el nivel ya ha terminado

    [SerializeField] private GameObject Victory;


    public GameObject victoryVFX; // VFX que se activará al final
    public AudioClip victoryClip; // Audio que se reproducirá al final
    public AudioClip startSceneClip;

    void Start()
    {
        FindClosestPortals(); // Buscar los portales más cercanos al iniciar
        StartCoroutine(SpawnEnemies());
        StartCoroutine(UpdateSpawnLimits());
        StartCoroutine(SpawnExtraEnemies());
        StartCoroutine(CheckAllPortalsDestroyed());
    }

    private void Awake()
    {
        FindClosestPortals(); // Buscar los portales más cercanos al iniciar
    }

    void FindClosestPortals()
    {
        GameObject[] allSpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        
        if (victoryVFX != null)
        {
            victoryVFX.SetActive(false); // Desactivar el VFX al inicio
        }
        if (AudioManager.instance != null && startSceneClip != null)
        {
            AudioManager.instance.PlaySFX(startSceneClip);
        }
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

        // Lista de índices para iterar por todos los spawners
        List<int> spawnerIndices = new List<int>();
        for (int i = 0; i < selectedSpawnerList.Count; i++)
        {
            spawnerIndices.Add(i);
        }

        // Mezclar los índices para seleccionar spawners aleatoriamente
        ShuffleList(spawnerIndices);

        foreach (int index in spawnerIndices)
        {
            GameObject spawner = selectedSpawnerList[index];

            if (spawner == null || !spawner.activeInHierarchy)
            {
                continue; // Ignorar spawners desactivados o nulos
            }

            // Obtener la posición actual del spawner
            Vector3 currentPosition = spawner.transform.position;

            // Validar si la posición es navegable
            if (NavMesh.SamplePosition(currentPosition, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
            {
                // Instanciar el enemigo en una posición válida
                GameObject enemy = EnemyPool.Instance.requestEnemy();
                enemy.transform.position = hit.position;
                enemy.transform.rotation = Quaternion.identity;
                Debug.Log($"Enemigo instanciado en posición válida cerca de {spawner.name}");
                return;
            }
        }

        // Si no se encuentra una posición válida, buscar fuera de la lista seleccionada
        Debug.LogWarning("No se encontró una posición válida en la lista seleccionada. Buscando otra posición válida...");

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
                Debug.Log($"Enemigo instanciado fuera de la lista inicial, en posición válida cerca de {spawner.name}");
                return;
            }
        }

        Debug.LogWarning("No se encontró ninguna posición válida dentro o fuera de la lista seleccionada.");
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
        StartCoroutine(FinishLevelWithEffects());
    }

    IEnumerator FinishLevelWithEffects()
    {
        Debug.Log("¡Todos los portales han sido destruidos! Nivel terminado.");

        // Activar el VFX si está asignado
        if (victoryVFX != null)
        {
            victoryVFX.SetActive(true);
        }

        // Reproducir el audio si está asignado
        if (AudioManager.instance != null && victoryClip != null)
        {
            AudioManager.instance.PlaySFX(victoryClip);
        }

        // Esperar la duración del audio o un tiempo fijo
        float delay = (victoryClip != null) ? victoryClip.length : 3f;
        yield return new WaitForSeconds(delay);

        // Cambiar de escena
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene(2);
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene(3);
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            Victory.SetActive(true);
        }
    }
}