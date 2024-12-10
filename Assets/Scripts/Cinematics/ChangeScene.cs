using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a cargar
    public float delay = 5f; // Tiempo en segundos antes de cambiar la escena

    private void Start()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena no está configurado.");
            return;
        }

        // Inicia el cambio de escena después de 'delay' segundos
        Invoke("ChangeScene", delay);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName); // Cambia a la escena especificada
    }
}
