using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleFalling : MonoBehaviour
{
    //Declarations
    [SerializeField] private List<GameObject> people;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPeople", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPeople()
    {
        Instantiate(people[SelectPeople()], RandomPosition(), RandomRotation());
    }

    private int SelectPeople()
    {
        return Random.Range(0, people.Count);
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-70, 71), Random.Range(20, 41), Random.Range(60, 80));
    }

    private Quaternion RandomRotation()
    {
        return new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }
}
