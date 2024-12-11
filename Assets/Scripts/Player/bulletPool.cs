using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPool : MonoBehaviour
{
    [Header("Bullet Pool Settings")]
    public GameObject bulletPrefab;
    private int poolSize = 60;
    public List<GameObject> bulletList;

    [Header("Audio Settings")]
    [SerializeField] AudioClip shootingClip; // Sonido de disparo
    

    private static bulletPool instance;
    public static bulletPool Instance { get { return instance; } }

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
                // Reproducir el sonido de disparo
                AudioManager.instance.PlaySFX(shootingClip);

                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }


        // Agregar una nueva bala al pool pero mantenerla desactivada
        AddBulletToPool(1);
        bulletList[bulletList.Count - 1].SetActive(false);
        return bulletList[bulletList.Count - 1];
    }

    private void PlaySound(AudioClip clip)
    {
        if (AudioManager.instance != null && clip != null)
        {
            AudioManager.instance.PlaySFX(clip);
        }
        
    }
}
