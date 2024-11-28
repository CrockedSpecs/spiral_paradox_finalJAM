using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float _moveSpeed = 3f;
    public float movePU = 1f;
    private Vector3 movement;

    private Rigidbody rb;
    private Camera cam;

    public Vector3 mousePos;
    public bool aiming;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Aiming();

    }

    void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position +  movement * _moveSpeed *movePU * Time.deltaTime);

    }

    void Aiming()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 direction = point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 25f));
        }
    }
}
