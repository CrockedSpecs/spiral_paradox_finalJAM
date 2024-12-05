using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardMovement : MonoBehaviour
{
    public float speed = 4;

    void FixedUpdate()
    {
        //Move other objects (pearls and fish)
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
