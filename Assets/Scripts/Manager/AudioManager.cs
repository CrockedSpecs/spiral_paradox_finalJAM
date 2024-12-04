using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Declarations
    [SerializeField] AudioSource musicSource, effectsSource;

    [SerializeField] AudioMixer audioMixer;

    //Create instance
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

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Play sound effect
    public void PlaySFX(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    //Play background music
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

    //Change MasterVolume
    public void MasterVolume(float sliderMusica)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderMusica) * 20);
    }

    //Change MusicVolume
    public void MusicVolume(float sliderMusica)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderMusica) * 20);
    }

    //Change SFXVolume
    public void SFXVolume(float sliderMusica)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderMusica) * 20);
    }
}
