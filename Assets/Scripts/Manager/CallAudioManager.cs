using UnityEngine;

public class CallAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPlaySFX(AudioClip audioClip)
    {
        AudioManager.instance.PlaySFX(audioClip);
    }

    public void CallPlayMusic(AudioClip audioClip)
    {
        AudioManager.instance.PlayMusic(audioClip);
    }

    //Change MasterVolume
    public void CallMasterVolume(float sliderMusica)
    {
        AudioManager.instance.MasterVolume(sliderMusica);
    }

    //Change MusicVolume
    public void CallMusicVolume(float sliderMusica)
    {
        AudioManager.instance.MusicVolume(sliderMusica);
    }

    //Change SFXVolume
    public void CallSFXVolume(float sliderMusica)
    {
        AudioManager.instance.SFXVolume(sliderMusica);
    }
}
