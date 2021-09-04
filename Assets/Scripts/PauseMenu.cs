using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;

	public string menuSceneName = "MainMenu";
	public Slider musicVolumeSlider;

	public SceneFader sceneFader;
	void Start ()
    {
		musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			AudioManager.instance.PlaySound("Button");
			Pause();
		}
	}

	public void Pause ()
	{
		
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		} 
		
		else
		{
			Time.timeScale = 1f;
		}

		
	}

	public void TryAgain ()
	{
		Pause();
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		Pause();
		AudioManager.instance.PlaySound("Button");
		sceneFader.FadeTo(menuSceneName);
	}

	public void updateMusicVolume()
	{
		PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
		AudioManager.instance.musicVolumeChanged();
	}
}
