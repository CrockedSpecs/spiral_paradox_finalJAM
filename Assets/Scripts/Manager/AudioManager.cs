using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Declarations
    [SerializeField] AudioSource musicSource, effectsSource;
    [SerializeField] AudioMixer audioMixer;

    // Create instance
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play sound effect
    public void PlaySFX(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    // Play background music
    public void PlayMusic(AudioClip music)
    {
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Change MasterVolume
    public void MasterVolume(float sliderMusica)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderMusica) * 20);
    }

    // Change MusicVolume
    public void MusicVolume(float sliderMusica)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderMusica) * 20);
    }

    // Change SFXVolume
    public void SFXVolume(float sliderMusica)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderMusica) * 20);
    }

    // Method to call when the "Play" button is pressed
    public void OnPlayButtonPressed()
    {
        StopMusic(); // Stop the background music
        // Optionally change the scene, if desired
        SceneManager.LoadScene("Cinematicas"); 
    }
}
