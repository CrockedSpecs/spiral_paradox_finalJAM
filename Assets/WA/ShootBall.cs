using UnityEngine;

public class ShootBall : MonoBehaviour
{
    //Declarations
    public GameObject Bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootBalls();
    }

    private void ShootBalls()
    {
        //Throw the pearls
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bullet, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        }
    }
}
