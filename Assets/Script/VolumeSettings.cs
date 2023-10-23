using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource myAudioSource;

    public void SetMusicVolume()
    {
        float vol = musicSlider.value;
        myAudioSource.volume = vol;
    }
}
