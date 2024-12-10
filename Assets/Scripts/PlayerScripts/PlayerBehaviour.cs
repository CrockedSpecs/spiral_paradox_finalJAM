using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] private float _moveSpeed = 5f;
    public float movePU = 1f;
    [SerializeField] private Vector3 movement;
    [SerializeField] private Animator animator;


    [Header("shooting")]
    public Transform bulletSpawner;
    public float shootInterval = 0.2f;
    private float shootTimer = 0f;
    public int initAmmo = 6;

    [Header("Aiming")]
    public Vector3 mousePos;
    public bool aiming;
    public GameObject weapon;
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody rb;

    public TextMeshProUGUI cursorText; // Referencia al texto del cursor
    public Vector2 offset = new Vector2(10, 10); // Desplazamiento del texto respecto al mouse
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()

    {
        _moveSpeed = 5f;

        Aiming();
        shootTimer += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            _moveSpeed = 2.5f;
            cursorText.text = $"{initAmmo}";
            if (shootTimer >= shootInterval && initAmmo > 0)
            {
                
                Shoot();
            }
            else if (initAmmo == 0 || Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(ReloadWeapon());
            }
        }


        PlayerMovement();

    }

    void PlayerMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement.normalized * _moveSpeed * movePU * Time.deltaTime);

        float rotationY = transform.rotation.eulerAngles.y;
        // Determina el estado según el movimiento y rotación del personaje.
        bool isMovingForward = false;
        bool isMovingBackward = false;

        // Verifica el rango de la rotación Y para determinar la dirección.
        if ((rotationY >= 315f || rotationY < 45f)) // Frente
        {
            isMovingForward = (movement.z == 1 || movement.x != 0);
            isMovingBackward = (movement.z == -1);
        }
        else if (rotationY >= 45f && rotationY < 135f) // Derecha
        {
            isMovingForward = (movement.x == 1 || movement.z != 0);
            isMovingBackward = (movement.x == -1);
        }
        else if (rotationY >= 135f && rotationY < 225f) // Atrás
        {
            isMovingForward = (movement.z == -1 || movement.x != 0);
            isMovingBackward = (movement.z == 1);
        }
        else if (rotationY >= 225f && rotationY < 315f) // Izquierda
        {
            isMovingForward = (movement.x == -1 || movement.z != 0);
            isMovingBackward = (movement.x == 1);
        }

        // Aplica los estados al Animator.
        if (isMovingForward)
        {
            animator.SetBool("isGoingFront", true);
            animator.SetBool("isGoingBack", false);
            animator.SetBool("isIdle", false);
        }
        else if (isMovingBackward)
        {
            animator.SetBool("isGoingFront", false);
            animator.SetBool("isGoingBack", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isGoingFront", false);
            animator.SetBool("isGoingBack", false);
            animator.SetBool("isIdle", true);
        }
    }

    void Aiming()
{
    if (cam == null)
    {
        cam = Camera.main;
    }

    Vector3 mousePos = Input.mousePosition;
    Ray ray = cam.ScreenPointToRay(mousePos);
    Plane groundPlane = new Plane(Vector3.up, transform.position); // Ajuste para el plano basado en el jugador
    float distance;
    if (groundPlane.Raycast(ray, out distance))
    {
        Vector3 point = ray.GetPoint(distance);
        Vector3 direction = point - bulletSpawner.position; // Ajuste basado en el spawner del arma
        direction.y = 0; // Mantén la dirección horizontal

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 25f));

        if (weapon != null)
        {
            weapon.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}


    
    void Shoot()
    {
            initAmmo--;
            shootTimer = 0f; // Reinicia el temporizador

            GameObject bullet = bulletPool.Instance.requestBullet();
            bullet.transform.position = bulletSpawner.position;
            bullet.transform.rotation = bulletSpawner.rotation;
            

    }

    IEnumerator ReloadWeapon()
    {
        cursorText.text = "R";
        yield return new WaitForSeconds(1f); // Simula el tiempo de recarga
        initAmmo = 6; // Restaura la munición al valor inicial
        cursorText.text = $"{initAmmo}";
        shootTimer = 0f; // Reinicia el temporizador de disparo
    }
}
