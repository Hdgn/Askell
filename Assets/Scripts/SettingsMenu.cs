using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

///Used to control the music and sound effects volumes
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    ///This sets all the volume for the whole game
    ///@param volume the master volume is stored
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    ///The game starts with the music volume set to 75% and the sound effects volume to 75%.
    ///@see AudioManager
    void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

    }


    ///The theme music volume is connected to a slider which updates the audio manager
    ///@see AudioManager
    public void updateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioManager.instance.musicVolumeChanged();
    }

    ///The sound effects volume is connected to a slider which updates the audio manager
    ///@see AudioManager
    public void updateEffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        AudioManager.instance.effectVolumeChanged();
    }


}


