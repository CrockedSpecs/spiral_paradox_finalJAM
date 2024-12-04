using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Vector3 offset;
    public Transform playerFollow;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 10, 0);
        playerFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFollow != null)
        {
            transform.position = playerFollow.position + offset;
        }
        else
        {
            playerFollow = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
