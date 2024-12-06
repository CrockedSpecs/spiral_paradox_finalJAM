using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class XpBehaviour : MonoBehaviour
{
    private float xpSpeed = 7f;
    private GameObject player;
    private bool isPlayerNear = false;
    private float minDistance = 2f;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        checkDistance();
        if (isPlayerNear)
        {
            MoveToPlayer();
        }
    }

    void checkDistance()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= minDistance)
        {
            isPlayerNear = true;
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, xpSpeed * Time.deltaTime);
    }
}