using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredTimeZone : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    //ForZones
    private float speedDecrease;
    private float radius;

    [SerializeField] private List<GameObject> zones;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radius = 3;
        speedDecrease = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
                zones[0].SetActive(true);
                break;
            case 2:
                radius = 6;
                break;
            case 3:
                speedDecrease = 3;
                break;
            case 4:
                radius = 8;
                break;
            case 5:
                zones[1].SetActive(true);
                break;
        }
    }
}
