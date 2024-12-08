using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Declarations
    private Vector3 bulletPosition;

    private int bulletDamage;
    private int bulletPenetration;

    [SerializeField] private ShootBullet subjectToObserver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPosition = transform.position;

        bulletDamage = 1;

        if (subjectToObserver != null)
        {
            subjectToObserver.bulletDamage += IncreaseBulletDamage;
            subjectToObserver.bulletPenetration += IncreaseBulletPenetration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(bulletPosition, transform.position) >= 40)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseBulletDamage()
    {
        bulletDamage++;
    }

    public void IncreaseBulletPenetration()
    {
        bulletPenetration++;
    }
}