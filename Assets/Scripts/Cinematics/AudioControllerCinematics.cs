using UnityEngine;

public class AudioManagerCinematics : MonoBehaviour
{
    [System.Serializable]
    public class TimedAudio
    {
        public AudioClip audioClip; // El clip de audio a reproducir
        public float playTime; // Tiempo en segundos para reproducir este clip
        [Range(0f, 1f)] public float volume = 1f; // Volumen de este clip (0 a 1)
    }

    public AudioSource audioSource; // Componente de AudioSource
    public TimedAudio[] audioSchedule; // Lista de sonidos programados

    private float startTime; // Momento en que inició la escena
    private int currentClipIndex = 0; // Índice del sonido actual

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("No se asignó un AudioSource.");
            return;
        }

        startTime = Time.time; // Guarda el tiempo de inicio de la escena
    }

    private void Update()
    {
        if (currentClipIndex < audioSchedule.Length)
        {
            float elapsedTime = Time.time - startTime; // Tiempo transcurrido

            // Comprueba si es el momento de reproducir el siguiente sonido
            if (elapsedTime >= audioSchedule[currentClipIndex].playTime)
            {
                PlayAudioClip(audioSchedule[currentClipIndex].audioClip, audioSchedule[currentClipIndex].volume);
                currentClipIndex++; // Avanza al siguiente sonido
            }
        }
    }

    private void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume); // Reproduce el sonido con el volumen especificado
        }
    }
}