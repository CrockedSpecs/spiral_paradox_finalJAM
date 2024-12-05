using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    public GameObject Bullet;

    //ForBullets
    private int bulletDamage;
    private int bulletPenetration;

    public Transform playerFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletDamage = 1;
        playerFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ShootBalls();
    }

    private void ShootBalls()
    {
        //Throw the pearls
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bullet, transform.position + new Vector3(0f, 1f, 0f), playerFollow.rotation);
        }
    }

    private void OnEnable()
    {
        if (skillLevel < 5)
        {
            UpdateSkill();
        }
    }

    private void UpdateSkill()
    {
        skillLevel++;

        switch (skillLevel)
        {
            default:

                break;
            case 1:
                bulletDamage = 1;
                break;
            case 2:
                bulletPenetration = 1;
                break;
            case 3:
                bulletDamage = 2;
                break;
            case 4:
                bulletPenetration = 2;
                break;
            case 5:
                bulletDamage = 3;
                bulletPenetration = 3;
                break;
        }
    }
}
