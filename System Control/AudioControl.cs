using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour {
    public AudioSource bgmAudioSource;
    public AudioSource soundAudioSource;

    public float bgmVolume=0.5f;
    public float soundVolume = 0.5f;

	public void bgmChangeVolume(Slider slider)
    {
        bgmAudioSource.volume = slider.value;
    }

    public void soundChangeVolume(Slider slider)
    {
        soundAudioSource.volume = slider.value;
    }
}
