using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    //Declarations
    public float movementSpeed;
    public float movementSpeedChange;

    [SerializeField] private Transform playerTransform;
    public bool isLive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeedChange = movementSpeed;

        isLive = true;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movment();
    }

    private void Movment()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeedChange * Time.deltaTime);
    }
}
