using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliderManager : MonoBehaviour
{
    public Slider MusicSlider;
    public Slider SFXSlider;

    void Start()
    {
        // Initialize sliders with current volume levels
        MusicSlider.value = AudioManager.Instance.musicSource.volume;
        SFXSlider.value = AudioManager.Instance.sfxSource.volume;

        MusicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    
}
