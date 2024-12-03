using System.Collections.Generic;
using UnityEngine;

public class BallsWall : MonoBehaviour
{
    //Declarations
    public int skillLevel;

    private int ballsNumber;
    private int ballsDamage;

    [SerializeField] private GameObject ball;
    [SerializeField] private List<GameObject> balls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballsNumber = 2;
        ballsDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        if (skillLevel < 4)
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
                balls.Add(Instantiate(ball, transform.position + Vector3.right * 3, ball.transform.rotation, transform));
                break;
            case 2:
                balls.Add(Instantiate(ball, transform.position + Vector3.right * 3, ball.transform.rotation, transform));
                break;
            case 3:
                balls.Add(Instantiate(ball, transform.position + Vector3.right * 3, ball.transform.rotation, transform));
                break;
            case 4:
                balls.Add(Instantiate(ball, transform.position + Vector3.right * 3, ball.transform.rotation, transform));
                break;
        }
    }
}