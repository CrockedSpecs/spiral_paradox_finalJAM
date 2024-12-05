using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeZone : MonoBehaviour
{
    //Declarations
    [SerializeField] private bool dynamic;
    [SerializeField] private float radius;
    public float speedDecrease;

    // Start is called before the first frame update
    void Start()
    {
        speedDecrease = 2;
    }

    void FixedUpdate()
    {
        if (dynamic)
        {
            ChangeRadius();
        }
        else
        {
            transform.localScale = new Vector3(radius, radius, radius);
        }
    }

    private void ChangeRadius()
    {
        if(radius < 8)
        {
            radius += 0.1f;
            transform.localScale = new Vector3(radius, radius, radius);
        }
        else
        {
            radius = 0;
            transform.localScale = new Vector3(radius, radius, radius);
        }
    }
}
