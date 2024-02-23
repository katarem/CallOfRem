using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    public Slider musicSlider;

    public GameObject MusicListener;

    AudioSource aud;
    void Start()
    {
        aud = MusicListener.gameObject.GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("volumeAudio",0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume",0.5f);
        aud.volume = musicSlider.value;
        AudioListener.volume = volumeSlider.value;
    }

    public void VolumeChangeSlider(float value)
    {
        PlayerPrefs.SetFloat("volumeAudio", value);
        AudioListener.volume = volumeSlider.value;
    }

    public void MusicChangeSlider(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        aud.volume = value;
    }
}
