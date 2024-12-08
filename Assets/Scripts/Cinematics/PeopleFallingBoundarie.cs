using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleFallingBoundarie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TurnOff();
    }

    private void TurnOff()
    {
        if (transform.position.y <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
