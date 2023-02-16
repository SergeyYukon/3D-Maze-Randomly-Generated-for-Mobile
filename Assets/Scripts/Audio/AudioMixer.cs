using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixerMaster;
    [SerializeField] private string[] namesMixer;
    [SerializeField] private Slider[] sliders;
    private string mixerName;

    private void Awake()
    {
        EventController.OnPauseEvent += PauseSounds;
        EventController.OnPlayEvent += PlaySounds;
    }

    private void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {                                 
            sliders[i].GetComponent<Slider>().value = PlayerPrefs.GetFloat(namesMixer[i], 1);
        }
    }

    public void ChangeVolume(float volume)
    {
        mixerMaster.audioMixer.SetFloat(mixerName, Mathf.Lerp(-20, 0, volume));
        if(volume == 0) mixerMaster.audioMixer.SetFloat(mixerName, -80);

        PlayerPrefs.SetFloat(mixerName, volume);
    }

    public void SelectMixer(string mixerName)
    {
        this.mixerName = mixerName;
    }

    private void PauseSounds()
    {
        mixerMaster.audioMixer.SetFloat("EffectsVolume", -80);
        mixerMaster.audioMixer.SetFloat("InGameMusic", -80); 
    }

    private void PlaySounds()
    {
        mixerMaster.audioMixer.SetFloat("EffectsVolume", 0);
        mixerMaster.audioMixer.SetFloat("InGameMusic", 0);
    }  
}
