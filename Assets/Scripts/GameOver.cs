using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	public void TryAgain()
	{
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(menuSceneName);
	}
}
