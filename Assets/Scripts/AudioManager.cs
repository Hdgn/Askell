using UnityEngine;
using System;
using UnityEngine.Audio;


///This class controls all of the audio excluding the button audio
public class AudioManager : MonoBehaviour
{

    ///Creates an array to store all the sound effects
    public Sound[] sounds;      // store all our sounds
    ///Creates an array to store all the theme music
    public Sound[] playlist;    // store all our music

    private int currentPlayingIndex = 999; // set high to signify no song playing
    private bool shouldPlayMusic = false;

    public static AudioManager instance; // will hold a reference to the first AudioManager created
    ///Links script to AudioMixer
    public AudioMixer mixer;

    private float mvol; // Global music volume
    private float evol; // Global effects volume
    public bool isMuted;


    //All music set to play at the start of the game
    private void Start()
    {
        //start the music
        PlayMusic();
        isMuted = PlayerPrefs.GetInt("Muted") == 1;
        AudioListener.pause = isMuted;

    }


    //Find the sources of the music and sets the initial volume for all sound effects and music
    //@param mvol The start up music volume
    //@param evol The start up sound effects volume
    private void Awake()
    {

        if (instance == null)
        {     // if the instance var is null this is first AudioManager
            instance = this;        //save this AudioManager in instance 
        }
        else
        {
            Destroy(gameObject);    // this isnt the first so destroy it
            return;                 // since this isn't the first return so no other code is run
        }

        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads

        // get preferences
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(sounds, evol);     // create sources for effects
        createAudioSources(playlist, mvol);   // create sources for music

    }

    //Creates the audio sources from attached clips adding suboptions volume, pitch and if it should loop
    //@param sounds Calls the sound array
    //@param volume sets volume of audio clip
    //@see Sound
    private void createAudioSources(Sound[] sounds, float volume)
    {
        foreach (Sound s in sounds)
        {   // loop through each music/effect
            s.source = gameObject.AddComponent<AudioSource>(); // create a new audio source
            s.source.clip = s.clip;     // the actual music/effect clip
            s.source.volume = s.volume * volume; // set volume based on parameter
            s.source.pitch = s.pitch;   // set the pitch
            s.source.loop = s.loop;     // should it loop
        }
    }

    ///Plays sound unless it does not pass set parameters in which case an error message is produced
    ///@param name The name of the audio
    public void PlaySound(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Unable to play sound " + name);
            return;
        }
        s.source.Play();
    }

    ///Plays music from playlist, randomising order they are called and sets their volume 
    public void PlayMusic()
    {
        if (shouldPlayMusic == false)
        {
            shouldPlayMusic = true;
            // pick a random song from our playlist
            currentPlayingIndex = UnityEngine.Random.Range(0, playlist.Length - 1);
            playlist[currentPlayingIndex].source.volume = playlist[0].volume * mvol; // set the volume
            playlist[currentPlayingIndex].source.Play(); // play it
        }

    }

    ///Stops music from being played, and resets playlist counter
    public void StopMusic()
    {
        if (shouldPlayMusic == true)
        {
            shouldPlayMusic = false;
            currentPlayingIndex = 999;
        }
    }


    //If the theme music ends, it either plays the next track or restarts the playlist
    void Update()
    {
        if (currentPlayingIndex != 999 && !playlist[currentPlayingIndex].source.isPlaying)
        {
            currentPlayingIndex++;
            if (currentPlayingIndex >= playlist.Length)
            {
                currentPlayingIndex = 0;
            }
            playlist[currentPlayingIndex].source.Play();
        }

       if (Input.GetKeyDown(KeyCode.M))
       {
            Mute();
       }
    }

    ///If the theme music volume is changed it updates all the audio sources
    public void musicVolumeChanged()
    {
        foreach (Sound m in playlist)
        {
            mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            m.source.volume = playlist[0].volume * mvol;
        }
    }

    ///If the sound effects volume is changed it updates all the audio sources
    public void effectVolumeChanged()
    {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * evol;
        }
        sounds[0].source.Play();
    }

    public void Mute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

}
