using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Declarations
    private int life;
    public float movementSpeed;

    [SerializeField] private GameObject hitEffect; // Prefab del efecto visual

    private GameObject player;
    private NavMeshAgent agent;

    private SpawnExperience spawnExperience;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        movementSpeed = agent.speed;

        spawnExperience = GameObject.FindGameObjectWithTag("SpawnExperience").GetComponent<SpawnExperience>();
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
            agent.speed /= other.GetComponent<TimeZone>().speedDecrease;
        }
        if (other.CompareTag("Zone2"))
        {
            agent.speed = 0;
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
            agent.speed = movementSpeed;
        }
        if (other.CompareTag("Zone2"))
        {
            agent.speed = other.GetComponent<TimeZone>().speedDecrease;
        }
    }

    // Método para reproducir el efecto visual
    public void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            // Instancia el efecto en la posición del enemigo
            hitEffect.transform.position = transform.position; 
            hitEffect.SetActive(true);
            HitEffectTurnOff();
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
            spawnExperience.ActivateExperience(transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }

    IEnumerator HitEffectTurnOff()
    {
        yield return new WaitForSeconds(4);
        // Apaga el efecto después de un tiempo
        hitEffect.SetActive(false);
    }
}
