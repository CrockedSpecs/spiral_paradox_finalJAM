using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        destroyFarBullet();
        transform.Translate(Vector3.forward* bulletSpeed * Time.deltaTime);
    }

    void destroyFarBullet()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > 15f)
        {
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Wall") ||  other.tag == ("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
