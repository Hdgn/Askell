using UnityEngine.Audio;
using System;
using UnityEngine;

///Stores information for each of our audio clips. In order to access our clips throughout the game we must store them and their settings together
[System.Serializable]
public class Sound
{
	///name stores the name of the music or sound effect
	public string name;     
	///clip stores the actual audio clip
	public AudioClip clip;  
	[Range(0f, 1f)]  
	///set the volume for the audio clip       
	public float volume;    
	[Range(0.1f, 3f)]  
	///pitch set the pitch for the audio clip    
	public float pitch;     
	[HideInInspector]       //Hide this variable from the Editor
	public AudioSource source;// the source that will play the sound
	public bool loop = false;// should this sound loop
}
