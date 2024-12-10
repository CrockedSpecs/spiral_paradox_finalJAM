using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class cursorTxtFollow : MonoBehaviour
{
    public TextMeshProUGUI cursorText; // Referencia al texto del cursor
    public Vector2 offset = new Vector2(10, 10); // Desplazamiento del texto respecto al mouse

    public GameObject player;
    private PlayerBehaviour playerBehaviour; // Referencia al script PlayerBehaviour

    private void Start()
    {
        // Encuentra el objeto con el tag "Player" y obtiene el componente PlayerBehaviour

        if (player != null)
        {
            playerBehaviour = player.GetComponent<PlayerBehaviour>();
        }

        if (cursorText == null)
        {
            Debug.LogError("CursorText no está asignado en el Inspector.");
        }

        if (playerBehaviour == null)
        {
            Debug.LogError("PlayerBehaviour no se encontró en el objeto con tag 'Player'.");
        }
    }

    private void Update()
    {
        // Actualiza la posición del texto del cursor
        Vector2 mousePosition = Input.mousePosition;
        cursorText.rectTransform.position = mousePosition + offset;

        // Actualiza el texto del cursor con el valor de initAmmo
        if (playerBehaviour != null)
        {
            if (playerBehaviour.initAmmo == 0)
            {
                cursorText.text = "R";
            }
            else 
            {
                cursorText.text = $"{playerBehaviour.initAmmo}";
            }


        }
    }

}
