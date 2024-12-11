using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTrap : MonoBehaviour
{
    //Declaration
    public int skillLevel;

    public GameObject Trap;

    //ForTraps
    public event Action trapDamage;
    private int putVelocity;
    public event Action trapRadius;

    // Start is called before the first frame update
    void Start()
    {
        putVelocity = 10;
        InvokeRepeating("PutTrapInFloor", 10, putVelocity);
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
                trapDamage?.Invoke();
                break;
            case 2:
                putVelocity = 7;
                break;
            case 3:
                trapRadius?.Invoke();
                break;
            case 4:
                putVelocity = 4;
                break;
            case 5:
                trapRadius?.Invoke();
                break;
        }
    }

    private void PutTrapInFloor()
    {
        Instantiate(Trap, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
    }
}
