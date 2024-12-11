using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Declarations
    private Vector3 bulletPosition;

    private int bulletDamage;
    private int bulletPenetration;

    [SerializeField] private Transform bulletInitialPosition;

    [SerializeField] private ShootBullet subjectToObserver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    }

    private void OnEnable()
    {
        transform.position = bulletInitialPosition.position;
        StartCoroutine(TimeToTurnOff());
    }

    public void IncreaseBulletDamage()
    {
        bulletDamage++;
    }

    public void IncreaseBulletPenetration()
    {
        bulletPenetration++;
    }

    IEnumerator TimeToTurnOff()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}