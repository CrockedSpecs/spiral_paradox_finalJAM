using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //Declarations
    [SerializeField] private int SpawnerLife = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnerLife <= 0)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Bullet"))
        {
            SpawnerLife--;
        }
    }
}
