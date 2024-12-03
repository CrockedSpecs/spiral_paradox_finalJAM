using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Declarations
    private Vector3 bulletPosition;

    private int bulletDamage;
    private int bulletPenetration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPosition = transform.position;

        bulletDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(bulletPosition, transform.position) >= 40)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBulletPenetration()
    {
        bulletPenetration--;
    }
}