using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //public static SFXManager singleton;
    
    [Header("---------------- Audio Source ----------------")]
    [SerializeField] AudioSource sfxSource;

    [Header("---------------- Audio Clip ----------------")]
    public AudioClip Watering;
    public AudioClip Click;
    public AudioClip Plant;

    /* void Awake()
    {
        singleton = this;
    } */
    
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
