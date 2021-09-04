using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Hint : MonoBehaviour
{
    public GameObject HintUi;
    public static bool HintOn;
    public string HintTxt;
    [SerializeField] private Text Hints;

    void Start()
    {
        HintOn = false;
    }

    public void HintB()
    {
        if (HintOn == false)
        {
            HintUi.SetActive(true);
            //AudioManager.instance.PlaySound("Button");
            Debug.Log("Hint");
            Hints.text = HintTxt;
            HintOn = true;
        }
        else
        {
            HintUi.SetActive(false);
           // AudioManager.instance.PlaySound("Button");
            HintOn = false;
        }

    }
}