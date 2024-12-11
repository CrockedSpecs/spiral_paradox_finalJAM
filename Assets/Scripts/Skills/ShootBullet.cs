using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    public List<GameObject> bullets;

    //ForBullets
    public event Action bulletDamage;
    public event Action bulletPenetration;

    public Transform playerFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("ShootBalls", 7, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootBalls()
    {
        switch (skillLevel)
        {
            default:

                break;
            case 1:
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                break;
            case 2:
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                bullets[2].SetActive(true);
                bullets[3].SetActive(true);
                break;
            case 3:
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                bullets[2].SetActive(true);
                bullets[3].SetActive(true);
                bullets[4].SetActive(true);
                bullets[5].SetActive(true);
                break;
            case 4:
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                bullets[2].SetActive(true);
                bullets[3].SetActive(true);
                bullets[4].SetActive(true);
                bullets[5].SetActive(true);
                bullets[6].SetActive(true);
                bullets[7].SetActive(true);
                break;
            case 5:
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                bullets[2].SetActive(true);
                bullets[3].SetActive(true);
                bullets[4].SetActive(true);
                bullets[5].SetActive(true);
                bullets[6].SetActive(true);
                bullets[7].SetActive(true);
                bullets[8].SetActive(true);
                bullets[9].SetActive(true);
                bullets[10].SetActive(true);
                bullets[11].SetActive(true);
                break;
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
                bullets[0].SetActive(true);
                bullets[1].SetActive(true);
                break;
            case 2:
                bullets[2].SetActive(true);
                bullets[3].SetActive(true);
                break;
            case 3:
                bullets[4].SetActive(true);
                bullets[5].SetActive(true);
                break;
            case 4:
                bullets[6].SetActive(true);
                bullets[7].SetActive(true);
                break;
            case 5:
                bullets[8].SetActive(true);
                bullets[9].SetActive(true);
                bullets[10].SetActive(true);
                bullets[11].SetActive(true);
                break;
        }
    }
}
