using System.Collections.Generic;
using UnityEngine;

public class ThornDroneWall : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    private float rotationSpeed;

    //ForDrones
    private int droneDamage;

    [SerializeField] private GameObject drone;
    [SerializeField] private List<GameObject> drones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = 50;
        droneDamage = 1;
    }

    void FixedUpdate()
    {
        RotateWall();
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
                drones[0].SetActive(true);
                drones[1].SetActive(true);
                break;
            case 2:
                droneDamage = 2;
                break;
            case 3:
                drones[2].SetActive(true);
                drones[3].SetActive(true);
                break;
            case 4:
                rotationSpeed = 100;
                break;
            case 5:
                drones[4].SetActive(true);
                drones[5].SetActive(true);
                drones[6].SetActive(true);
                drones[7].SetActive(true);
                break;
        }
    }

    private void RotateWall()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}