using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is used when the LevelWonUI has been called. It assigns each of the buttons ti's individually desired functionality.
public class CompleteLevel : MonoBehaviour {

	public string menuSceneName = "Menu";
	[SerializeField] public GameManager gameManager;

	public SceneFader sceneFader;

	//Loads the next level available
	public void Continue ()
	{
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(gameManager.nextLevel);
	}

	//Takes the user back to the Level Select Scene
	public void Menu ()
	{
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(menuSceneName);
	}

	//Restarts the level
	public void TryAgain()
	{
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

}
