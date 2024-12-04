using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //Declarations
    [SerializeField] private GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 10, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, enemy.transform.rotation);
    }
}
