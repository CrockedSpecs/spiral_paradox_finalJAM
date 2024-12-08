using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredTimeZone : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    //ForZones
    public event Action speedDecrease;
    public event Action radius;

    [SerializeField] private List<GameObject> zones;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                radius?.Invoke();
                break;
            case 3:
                speedDecrease?.Invoke();
                break;
            case 4:
                radius?.Invoke();
                break;
            case 5:
                zones[1].SetActive(true);
                break;
        }
    }
}
