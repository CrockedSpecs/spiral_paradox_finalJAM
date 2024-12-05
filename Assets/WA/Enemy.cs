using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private GameObject player;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }


    void followPlayer()
    {
        agent.destination = player.transform.position;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if (other.CompareTag("FireBall"))
        {
            Destroy(gameObject);
        }
    }
}
