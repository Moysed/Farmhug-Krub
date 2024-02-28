using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlay : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject ObjMusic;

    //Value from slider, and it converts to volume level
    public float MusicVolume;
    private AudioSource AudioSource;

    // Start is called before the first frame update
    private void Start()
    {
        ObjMusic = GameObject.FindWithTag("BGM");
        AudioSource = ObjMusic.GetComponent<AudioSource>();

        //Set Volume
        MusicVolume = PlayerPrefs.GetFloat("volume");
        MusicVolume = 0.8f;
        AudioSource.volume = MusicVolume;
        volumeSlider.value = MusicVolume;
    }

    // Update is called once per frame
    private void Update()
    {
        AudioSource.volume = MusicVolume;
        PlayerPrefs.SetFloat("volume", MusicVolume);
    }

    public void VolumeUpdate(float volume)
    {
        MusicVolume = volume;
    }

    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volume");
        AudioSource.volume = 1;
        volumeSlider.value = 1;
    }
}
