using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private float bulletSpeed = 8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //GameObject bullet = Instantiate();
    }
}
