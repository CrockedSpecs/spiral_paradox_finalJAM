using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Declarations
    private int life;

    private EnemyFollowPlayer enemyFollowPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = 1;

        enemyFollowPlayer = GetComponent<EnemyFollowPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ChangeLife();
            other.GetComponent<Bullet>().ChangeBulletPenetration();
        }
        if (other.CompareTag("ThornDrone"))
        {
            ChangeLife();
        }
        if (other.CompareTag("Zone1"))
        {
            enemyFollowPlayer.movementSpeedChange /= other.GetComponent<TimeZone>().speedDecrease;
        }
        if (other.CompareTag("Zone2"))
        {
            enemyFollowPlayer.movementSpeedChange = 0;
        }
        if (other.CompareTag("TrapArea"))
        {
            ChangeLife();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone1"))
        {
            enemyFollowPlayer.movementSpeedChange = enemyFollowPlayer.movementSpeed;
        }
        if (other.CompareTag("Zone2"))
        {
            enemyFollowPlayer.movementSpeedChange = other.GetComponent<TimeZone>().speedDecrease;
        }
    }

    private void ChangeLife()
    {
        life--;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
