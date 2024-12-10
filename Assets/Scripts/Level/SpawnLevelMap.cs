using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpawnLevelMap : MonoBehaviour
{
    //Declarations
    private string biomas;
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

        biomas = SceneManager.GetActiveScene().name;

        for (int index = 0; index < spawns.Count; index++)
        {
            switch (biomas)
            {
                default:

                    break;
                case "Level1":
                    levelMapsIndex = Random.Range(0, levelKingdomMaps.Count);
                    Instantiate(levelKingdomMaps[levelMapsIndex], spawns[index].transform.position, spawns[index].transform.rotation);
                    break;
                case "Level2":
                    levelMapsIndex = Random.Range(0, levelVikingMaps.Count);
                    Instantiate(levelVikingMaps[levelMapsIndex], spawns[index].transform.position, spawns[index].transform.rotation);
                    break;
                case "Level3":
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
