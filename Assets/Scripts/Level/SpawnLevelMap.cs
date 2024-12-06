using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class SpawnLevelMap : MonoBehaviour
{
    //Declarations
    private int biomas;
    [SerializeField] private List<GameObject> spawns;
    [SerializeField] private List<GameObject> levelKingdomMaps;
    [SerializeField] private List<GameObject> levelVikingMaps;
    [SerializeField] private List<GameObject> levelCityMaps;
    private NavMeshSurface navMeshSurface;

    [SerializeField] private GameObject playerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject navMeshObject = GameObject.Find("NavMesh Surface");
        navMeshSurface = navMeshObject.GetComponent<NavMeshSurface>();
        StartCoroutine(levelInit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectLevelMaps()
    {
        int levelMapsIndex = 0;

        for (int index = 0; index < spawns.Count; index++)
        {
            switch (biomas)
            {
                default:

                    break;
                case 0:
                    levelMapsIndex = Random.Range(0, levelKingdomMaps.Count);
                    Instantiate(levelKingdomMaps[levelMapsIndex], spawns[index].transform.position, spawns[index].transform.rotation);
                    break;
                case 1:
                    levelMapsIndex = Random.Range(0, levelVikingMaps.Count);
                    Instantiate(levelVikingMaps[levelMapsIndex], spawns[index].transform.position, spawns[index].transform.rotation);
                    break;
                case 2:
                    levelMapsIndex = Random.Range(0, levelCityMaps.Count);
                    Instantiate(levelCityMaps[levelMapsIndex], spawns[index].transform.position, spawns[index].transform.rotation);
                    break;
            }
        }
    }

    private void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }

    private void SpawnPlayer()
    {
        // Define a starting position
        Vector3 startPosition = Vector3.zero; // Adjust as needed

        // Search for a valid position within the NavMesh
        if (FindRandomPointOnNavMesh(startPosition, 10f, out Vector3 spawnPosition))
        {
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Failed to find a valid spawn point for the player.");
        }
    }

    private bool FindRandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        // Generate a random point within a sphere
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        // Check if it's on the NavMesh
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private IEnumerator levelInit()
    {
        SelectLevelMaps();
        yield return new WaitForSeconds(0.0001f);
        BakeNavMesh();
        yield return new WaitForSeconds(0.0001f);
        SpawnPlayer();
    }
}
