using System;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    public GameObject Bullet;

    //ForBullets
    public event Action bulletDamage;
    public event Action bulletPenetration;

    public Transform playerFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                bulletPenetration?.Invoke();
                break;
            case 2:
                bulletDamage?.Invoke();
                break;
            case 3:
                bulletPenetration?.Invoke();
                break;
            case 4:
                bulletDamage?.Invoke();
                break;
            case 5:
                bulletPenetration?.Invoke();
                break;
        }
    }
}
