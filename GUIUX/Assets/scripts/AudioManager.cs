using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider ambientSlider;
    public Slider sfxSlider;
    public static AudioManager Instance;

    public Audio[] ambient, sfxSounds;
    public AudioSource ambientSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayAmbient("Ambient");

        if(PlayerPrefs.HasKey("masterVol") || PlayerPrefs.HasKey("sfxVol") || PlayerPrefs.HasKey("ambientVol"))
        {
            LoadVolume();
        }
        else
        {
            MasterVolume();
            AmbientVolume(ambientSlider.value);
            SFXVolume(sfxSlider.value);
        }
    }

    public void PlayAmbient(string name)
    {
        Audio s = Array.Find(ambient, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            ambientSource.clip = s.clip;
            ambientSource.Play();
        }
    }

    public void PlaySfX(string name)
    {
        Audio s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void AmbientVolume(float volume)
    {
        ambientSource.volume = volume;
        PlayerPrefs.SetFloat("ambientVol", volume);
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("sfxVol", volume);
    }
    public void MasterVolume()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("masterVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVol", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVol");
        ambientSlider.value = PlayerPrefs.GetFloat("ambientVol");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");

    }
}
