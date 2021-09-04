//Game Manager Input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;
	[SerializeField] public QuizManager quizManager;
	[SerializeField] public Drag_DropManager dragDropManager;
	public float scorePercent = 0;
	public float scorePercent2 = 0;

	public LevelType levelType;   //type of level

	private string finaleSceneName = "Finale"; //changed from public
	public string nextLevel;
	public int levelToUnlock;
	public string currentLevel;
	public TMP_Text LevelNo;
	private int c;

	public SceneFader sceneFader;

	void Start()
	{
		GameIsOver = false;
		completeLevelUI.SetActive(false);
		LevelNo.text = currentLevel;

		//Sets the player prefernences linked to the level selector
		int levelReached = PlayerPrefs.GetInt("levelReached", 1);
		Debug.Log("Unlocked Levels =" + PlayerPrefs.GetInt("levelReached"));
	}

	// Update is called once per frame
	void Update()
	{
		if (GameIsOver)
			return;

		//Players may leave the game by pressing Esc or Q on their keyboard
		if (Input.GetKey("q") || Input.GetKey("escape"))
		{
			Application.Quit();
			Debug.Log("Quit By Key");
		}
	}

	//Class called to end game. It calls the correct UI screen within the game,
	//depending on if their score percent is high enough
	public void EndGame()
	{
		//scorePercent = (quizManager.Score / quizManager.totalQuestions) * 100;
		//scorePercent2 = (dragDropManager.Score / dragDropManager.totalQuestions) * 100;


		//Depending on the type of level the score is assigned to the player preference of this level.
		//This prevents them from being overwritten by levels which have not been completed and therefore would
		//display 0/0.
		switch (levelType)
		{
			case LevelType.QUIZ:
				PlayerPrefs.SetString(currentLevel, quizManager.Score + "/" + quizManager.totalQuestions);
				scorePercent = (quizManager.Score / quizManager.totalQuestions) * 100;
				break;
			case LevelType.DRAG:
				PlayerPrefs.SetString(currentLevel, dragDropManager.Score + "/" + dragDropManager.totalQuestions);
				scorePercent2 = (dragDropManager.Score / 4) * 100;
				break;
		}

		if (scorePercent >= 80 || scorePercent2 >= 50)
		{
			WinLevel();

		}
		else
		{
			//Calls the GameOver UI screen and plays an explosion sound clip.
			//Depending on the level type the score is assigned to the level's Text box
			GameIsOver = true;
			gameOverUI.SetActive(true);
			Debug.Log("Game is Over");
			AudioManager.instance.PlaySound("Explosion");
			switch (levelType)
			{
				case LevelType.QUIZ:
					quizManager.ScoreTxt.text = quizManager.Score + "/" + quizManager.totalQuestions;
					break;
				case LevelType.DRAG:
					dragDropManager.ScoreTxt.text = dragDropManager.Score + "/" + dragDropManager.totalQuestions;
					break;
			}
		}
	}

	public void WinLevel()
	{
		AudioManager.instance.PlaySound("Applause");
		c = int.Parse(currentLevel);
		Debug.Log("CURRENT LEVEL PARSE " + c);
		if (c == levelToUnlock)
		{
			sceneFader.FadeTo(finaleSceneName);

		}

		else
		{
			//Calls the LevelWon UI screen and plays an applause sound clip.
			//Depending on the level type the score is assigned to the level's Text box
			GameIsOver = true;
			completeLevelUI.SetActive(true);
			Debug.Log("Yay you won!");
			switch (levelType)
			{
				case LevelType.QUIZ:
					quizManager.winScoreTxt.text = quizManager.Score + "/" + quizManager.totalQuestions;
					break;
				case LevelType.DRAG:
					dragDropManager.winScoreTxt.text = dragDropManager.Score + "/" + dragDropManager.totalQuestions;
					break;
			}

			//Unlocks the next level in the level selector
			PlayerPrefs.SetInt("levelReached", levelToUnlock);

			if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
			{
				PlayerPrefs.SetInt("levelReached", levelToUnlock);
				Debug.Log("Add Level Unlocked");
			}
		}
	}
}
