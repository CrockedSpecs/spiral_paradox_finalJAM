using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Declarations
    private Rigidbody rb;

    [SerializeField] private GameObject area; // Área de daño
    [SerializeField] private GameObject activationEffect; // Efecto al activar el área
    [SerializeField] private GameObject destructionEffect; // Efecto al destruir la trampa

    [SerializeField] private int damage; // Efecto al destruir la trampa

    [SerializeField] private PutTrap subjectToObserver;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Aplica fuerza inicial a la trampa
        rb.AddForce(transform.up * 2);

        // Inicia la secuencia de activación y destrucción
        StartCoroutine(TimeToDamage());

        if (subjectToObserver != null)
        {
            subjectToObserver.trapDamage += IncreaseDamage;
            subjectToObserver.trapRadius += IncreaseRadius;
        }
    }

    IEnumerator TimeToDamage()
    {
        // Espera 3 segundos antes de activar el área
        yield return new WaitForSeconds(3);

        // Reproduce el efecto de activación (si está configurado)
        if (activationEffect != null)
        {
            Instantiate(activationEffect, transform.position, Quaternion.identity);
        }

        // Activa el área de daño
        area.SetActive(true);

        // Espera 1 segundo antes de destruir la trampa
        yield return new WaitForSeconds(1);

        // Reproduce el efecto de destrucción (si está configurado)
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }

        // Destruye la trampa
        Destroy(gameObject);
    }

    private void IncreaseDamage()
    {
        damage++;
    }

    private void IncreaseRadius()
    {
        area.GetComponent<SphereCollider>().radius ++;
    }
}
