using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //Declarations
    private Rigidbody rb;

    [SerializeField] private GameObject area;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.up * 2);

        StartCoroutine(TimeToDamage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeToDamage()
    {
        yield return new WaitForSeconds(3);
        area.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
