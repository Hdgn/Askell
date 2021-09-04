//LevelSelector Input
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

	public SceneFader fader;
	//An arracy is used to store the level buttons.
	public Button[] levelButtons;
	public Button FinishButton;

	void Start()
	{
		//Checks whether there already exist a player preference names "levelReached", if not one is created.
		if(PlayerPrefs.HasKey("levelReached"))
        {
			PlayerPrefs.GetInt("levelReached");
			Debug.Log(PlayerPrefs.GetInt("levelReached"));
		}

        else { 
			PlayerPrefs.SetInt("levelReached", 1);
			PlayerPrefs.Save();
		}

		//The player preference stores an integer which is set within the level. This checks the preference
		//and assigns it to an integer variable.
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);

		//The button's position in the array is the checks and if it equall to the levelReached interger plus one
		//the button is locked by preventing interactions. By check if it is equal + 1 it ensures that the next level 
		//is unlocked to the user otherwise they would not be able to move forward.
		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
				levelButtons[i].interactable = false;
		}

		//if the player reaches the final level the Finish button containing the end scene is made accessible.
		if (PlayerPrefs.GetInt("levelReached") == levelButtons.Length)
		{
			FinishButton.interactable = true;
		}
		else
		{
			FinishButton.interactable = false;
		}
	}

	//Each button calls this class when click, the string levelName is then altered within unity's Inspector to the 
	//scene level's name.
	public void Select(string levelName)
	{
		fader.FadeTo(levelName);
		Time.timeScale = 1;
		AudioManager.instance.PlaySound("Button");

	}

	//The finish button is set up to load the Finale scene when clicked.
	public void Finish(string finale)
    {
		fader.FadeTo(finale);
		Time.timeScale = 1;
		AudioManager.instance.PlaySound("Button");
		AudioManager.instance.PlaySound("Applause");


	}


}
