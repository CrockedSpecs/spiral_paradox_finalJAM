using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Declarations
    private int life;
    private EnemyFollowPlayer enemyFollowPlayer;

    [SerializeField] private GameObject experience;
    [SerializeField] private GameObject hitEffect; // Prefab del efecto visual

    private GameObject player;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = 1;
        enemyFollowPlayer = GetComponent<EnemyFollowPlayer>();

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
            ChangeLife();
            //other.GetComponent<Bullet>().ChangeBulletPenetration();
            other.gameObject.SetActive(false);
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

    // Método para reproducir el efecto visual
    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            // Instancia el efecto en la posición del enemigo
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            // Destruye el efecto después de un tiempo
            Destroy(effect, 4f); 
        }
    }

    // Método para reducir la vida del enemigo y activar el efecto visual
    private void ChangeLife()
    {
        life--;
        PlayHitEffect(); // Activa el efecto cada vez que el enemigo pierde vida

        if (life <= 0)
        {
            // Instancia experiencia y destruye el enemigo si la vida llega a 0
            Instantiate(experience, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), experience.transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
