using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public GameObject knightPrefab;
    public GameObject vikingPrefab;
    public GameObject policePrefab;
    private GameObject chosenEnemyPrefab;
    
    public List<GameObject> EnemyList;
    public int enemyChoice;
    private int poolSize = 60;

    private static EnemyPool instance;
    public static EnemyPool Instance { get { return instance; } }
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (enemyChoice == 0)
        {
            chosenEnemyPrefab = knightPrefab;
        }
        else if (enemyChoice == 1)
        {
            chosenEnemyPrefab = vikingPrefab;
        }
        else if (enemyChoice == 2)
        {
            chosenEnemyPrefab = policePrefab;
        }


        AddEnemyToPool(poolSize, chosenEnemyPrefab);
    }

    private void AddEnemyToPool(int amount, GameObject enemyPrefab)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            EnemyList.Add(enemy);
            enemy.transform.parent = transform;
        }
    }

    public GameObject requestEnemy()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (!EnemyList[i].activeSelf)
            {
                EnemyList[i].SetActive(true);
                return EnemyList[i];
            }

        }
        AddEnemyToPool(1, chosenEnemyPrefab);
        EnemyList[EnemyList.Count - 1].SetActive(false);
        return EnemyList[EnemyList.Count - 1];
    }
}
