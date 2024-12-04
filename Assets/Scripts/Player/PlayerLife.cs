using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    //Declarations
    private int life;
    private bool immunity;

    [SerializeField] private List<GameObject> lifesUI;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && !immunity)
        {
            immunity = true;

            life--;

            lifesUI[life].SetActive(false);

            if (life <= 0)
            {
                GameManager.Instance.PauseGame();
                pauseButton.SetActive(false);
                gameOver.SetActive(true);
            }

            StartCoroutine(TimeImmunity());
        }   
    }

    IEnumerator TimeImmunity()
    {
        yield return new WaitForSeconds(1);
        immunity = false;
    }
}
