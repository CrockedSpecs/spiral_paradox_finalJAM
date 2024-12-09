using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    private int poolSize = 60;
    public List<GameObject> bulletList;

    private static bulletPool instance;
    public static bulletPool Instance { get { return instance; } }
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        AddBulletToPool(poolSize);
    }

    private void AddBulletToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject requestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }

        }
        AddBulletToPool(1);
        bulletList[bulletList.Count - 1].SetActive(false);
        return bulletList[bulletList.Count - 1];
    }

}
