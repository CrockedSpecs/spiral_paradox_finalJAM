using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Declarations
    private Vector3 bulletPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.z - bulletPosition.z) > 10 || (transform.position.z - bulletPosition.z) < 10)
        {
            Destroy(gameObject);
        }
    }
}
