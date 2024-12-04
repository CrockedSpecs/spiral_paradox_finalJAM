using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTrap : MonoBehaviour
{
    //Declaration
    public int skillLevel;

    public GameObject Trap;

    //ForTraps
    private int trapDamage;
    private int putVelocity;
    private int trapRadius;

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
                trapDamage = 1;
                break;
            case 2:
                trapDamage = 2;
                break;
            case 3:
                trapRadius = 3;
                break;
            case 4:
                putVelocity = 5;
                break;
            case 5:
                trapRadius = 6;
                break;
        }
    }

    private void PutTrapInFloor()
    {
        Instantiate(Trap, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
    }
}
