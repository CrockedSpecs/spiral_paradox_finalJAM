using System.Collections.Generic;
using UnityEngine;

public class SpawnLevelMap : MonoBehaviour
{
    //Declarations
    private int biomas;
    [SerializeField] private List<GameObject> spawns;
    [SerializeField] private List<GameObject> levelKingdomMaps;
    [SerializeField] private List<GameObject> levelVikingMaps;
    [SerializeField] private List<GameObject> levelCityMaps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectLevelMaps();
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
}
