using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;


    public AudioClip backgroundMusic;


    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        musicSource.volume = 0.5f; // Set default volume
    }

    void Start(){
        PlayBackgroundMusic(backgroundMusic);
    }

    public void PlayBackgroundMusic(AudioClip backgroundMusic)
    {
        if (musicSource.clip == backgroundMusic) 
        return; // Avoid restarting the same music

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }



}
