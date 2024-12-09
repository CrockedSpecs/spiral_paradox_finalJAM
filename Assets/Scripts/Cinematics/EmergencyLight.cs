using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light directionalLight; // Referencia a la luz direccional
    public float intensityChangeInterval = 2f; // Intervalo de cambio de intensidad en segundos
    public float maxIntensity = 2f; // Máxima intensidad
    public float minIntensity = 0f; // Mínima intensidad

    private bool isActive = false; // Indica si la luz está activa
    private float nextChangeTime = 0f;

    private void Start()
    {
        if (directionalLight == null)
        {
            Debug.LogError("No se asignó una luz direccional.");
            return;
        }

        directionalLight.enabled = false; // La luz empieza apagada
        Invoke("ActivateLight", 20f); // Activa la luz después de 20 segundos
    }

    private void ActivateLight()
    {
        directionalLight.enabled = true; // Activa la luz
        isActive = true; // Cambia el estado a activo
        nextChangeTime = Time.time + intensityChangeInterval;
    }

    private void Update()
    {
        if (isActive && Time.time >= nextChangeTime)
        {
            // Alterna entre la intensidad máxima y mínima
            directionalLight.intensity =
                directionalLight.intensity == maxIntensity ? minIntensity : maxIntensity;

            nextChangeTime = Time.time + intensityChangeInterval; // Actualiza el tiempo del próximo cambio
        }
    }
}
