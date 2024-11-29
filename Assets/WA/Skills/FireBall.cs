using UnityEngine;

public class FireBall : MonoBehaviour
{
    //Declarations
    [SerializeField] private float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveFireWall();
    }

    private void MoveFireWall()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
