using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    //public AudioMixer _audio;
    public AudioClip _audio;
    public Slider slider;
    //public GameObject on;
    //public GameObject off;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else
        {
            Load();
        }
    }

    public void SetVolume(/*float vol*/)
    {
        AudioListener.volume = slider.value;
    }

    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("musicVolume");
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }

    /* public void On()
    {
        AudioListener.volume = 0;
        on.SetActive(false);
        off.SetActive(true);
    }

    public void Off()
    {
        AudioListener.volume = 1;
        on.SetActive(true);
        off.SetActive(false);
    } */
}
