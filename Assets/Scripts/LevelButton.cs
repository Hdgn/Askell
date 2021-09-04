using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Used by the level button prefab to call the next level, and assign final score to button.
public class LevelButton : MonoBehaviour
{
    public Text scoreText;
    public string Level;


    void Start()
    {
        if (PlayerPrefs.HasKey(Level))
        {
            PlayerPrefs.GetString(Level);
           // Debug.Log(PlayerPrefs.GetString(Level));
        }

        else
        {
            PlayerPrefs.SetString(Level, "");
            PlayerPrefs.Save();
        }

        scoreText.text = PlayerPrefs.GetString(Level);
       
    }
    
}


