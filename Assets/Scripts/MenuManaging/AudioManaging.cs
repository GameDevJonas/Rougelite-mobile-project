using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioManaging : MonoBehaviour
{
    public AudioMixer mixer;

    [Range(-80f, 20f)]
    public float sFXVolume, musicVolume;

    public Slider sFXSlider, musicSlider;

    public void UpdateVolume()
    {

        mixer.SetFloat("MainMusic", musicVolume);

        mixer.SetFloat("SFX", sFXVolume);

    }

    private void Update()
    {
        sFXVolume = sFXSlider.value;
        musicVolume = musicSlider.value;
    }
}
