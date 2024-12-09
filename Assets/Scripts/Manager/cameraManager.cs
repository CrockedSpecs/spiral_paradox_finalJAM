using UnityEngine;
using Cinemachine;

public class CameraBoundsController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera; // La cámara de Cinemachine
    public Transform player; // El jugador
    public Vector3 minBounds; // Límite mínimo del mapa (x, y)
    public Vector3 maxBounds; // Límite máximo del mapa (x, y)

    private CinemachineTransposer transposer;

    public GameObject theCamera;

    void Start()
    {
        if (cinemachineCamera != null)
        {
            transposer = cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
    }

    void Update()
    {
        if (player != null && cinemachineCamera != null)
        {
            // Verifica si el jugador está fuera de los límites en el eje X o Y
            bool isOutOfBoundsX = player.position.x < minBounds.x || player.position.x > maxBounds.x;
            bool isOutOfBoundsZ = player.position.z < minBounds.z || player.position.z > maxBounds.z;

            if (isOutOfBoundsX || isOutOfBoundsZ)
            {
                // Desactiva el seguimiento de la cámara si el jugador está fuera de los límites
                if (cinemachineCamera.Follow != null)
                {
                    cinemachineCamera.Follow = null;
                }

                // Si se pasa el límite en X, ajusta la posición de la cámara en X al jugador
                if (isOutOfBoundsX)
                {
                    theCamera.transform.position = new Vector3(theCamera.transform.position.x, theCamera.transform.position.y, player.transform.position.z);
                }

                // Si se pasa el límite en Y, ajusta la posición de la cámara en Y al jugador
                if (isOutOfBoundsZ)
                {
                    theCamera.transform.position = new Vector3(player.transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z);

                }
            }
            else
            {
                // Activa el seguimiento si el jugador está dentro de los límites
                if (cinemachineCamera.Follow == null)
                {
                    cinemachineCamera.Follow = player;
                }
            }
        }
    }
}
