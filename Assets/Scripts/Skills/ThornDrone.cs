using UnityEngine;

public class ThornDrone : MonoBehaviour
{
    //Declarations
    public int droneDamage;

    [SerializeField] private ThornDroneWall subjectToObserver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        droneDamage = 1;

        if (subjectToObserver != null)
        {
            subjectToObserver.droneDamage += IncreaseDamage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseDamage()
    {
        droneDamage++;
    }
}
