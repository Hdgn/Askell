//Timer Input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This class set the timers display to the user and reduces the initial time every second. If the timer reaches 0 whilst the
//user is playing the game the game is ended.
public class Timer : MonoBehaviour
{
    [SerializeField] private float timeValue = 30f;
    [SerializeField] public GameManager gameManager;
    public TMP_Text timerText;

    // Update is called once per frame
    //Every fram the timer either remove a second from the timer or if has reach 0 seconds the game is ended using the game manager.
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            gameManager.EndGame();

        }

        DisplayTime(timeValue);
    }

    //The timer's text is updated every second, this class includes prevents a glitch that can sometimes occur over the frames update,
    //with the game ending at 1 second displayed to the user with the game registering 0 seconds immediately. This allows the user to
    //see the display of 0 seconds as the game ends. It also sets the format of the timer output to 00:00. Whilst the minutes is not 
    //currently used in the game it means this option is open as the game develops and is clear to understand for the user as a standerd display.
    void DisplayTime (float currentTime)
    {
        if (currentTime < 0)
        {
            currentTime = 0;
        }
        else if (currentTime > 0)
        {
            currentTime += 1;
        }

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
