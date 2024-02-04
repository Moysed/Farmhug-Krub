using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer _audio;
    public GameObject on;
    public GameObject off;

    public void SetVolume(float vol)
    {
        _audio.SetFloat("vol", vol);
    }

    public void On()
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
    }
}
