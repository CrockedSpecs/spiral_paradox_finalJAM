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

    [SerializeField] private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject navMeshObject = GameObject.Find("NavMesh Surface");
        navMeshSurface = navMeshObject.GetComponent<NavMeshSurface>();
        StartCoroutine(levelInit());
        player.SetActive(false);
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


    private IEnumerator levelInit()
    {
        SelectLevelMaps();
        yield return new WaitForSeconds(0.0001f);
        BakeNavMesh();
        yield return new WaitForSeconds(0.0001f);
        player.SetActive(true);
    }
}
