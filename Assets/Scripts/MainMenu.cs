using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad;
	public string credits;
	public static bool Settings;
	public GameObject SettingsUi;
	public GameObject MenuUi;
	public SceneFader sceneFader;

	void Start()
    {
		//PlayerPrefs.DeleteAll();
		Debug.Log("Reset Levels");
		Settings = false;

	}

	public void PlayGame ()
	{
		AudioManager.instance.PlaySound("Button"); 
		sceneFader.FadeTo(levelToLoad);
	}

	public void QuitGame ()
	{
		Debug.Log("QUIT!");
		AudioManager.instance.PlaySound("Button");
		Application.Quit();
	}

	public void Credits()
    {
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(credits);
	}


	public void SettingsMenu()
	{
		if (Settings == false)
		{
			AudioManager.instance.PlaySound("Button");
			SettingsUi.SetActive(true);
			MenuUi.SetActive(false);
			Debug.Log("Settings Menu");
			Settings = true;
		}
		else
		{
			AudioManager.instance.PlaySound("Button");
			SettingsUi.SetActive(false);
			MenuUi.SetActive(true);
			Settings = false;
		}

	}

}

