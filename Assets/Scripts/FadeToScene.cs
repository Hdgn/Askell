using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeToScene : MonoBehaviour
{
	public SceneFader sceneFader;

	public void callLevel(string level)
    {
		AudioManager.instance.PlaySound("Button");
		Time.timeScale = 1;
		sceneFader.FadeTo(level);
    }
}

