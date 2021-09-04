//Answer Input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Assigned to each button this script checks whether the button's assigned answer is correct depending on whether it's position in
//the array is equal to the correct answer integer. It then calls the correct answer repsponse or incorrect answer response from
//within the quiz manager.
public class AnswerScript : MonoBehaviour
{
   public bool isCorrect = false;
   public QuizManager quizManager;

   public Color initialColour;


    private void Start()
    {
        initialColour = GetComponent<Image>().color;
    }

    //A colour reponse is also added to the button to indicate to the user if the answer is correct or incorrect.
   public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            GetComponent<Image>().color = Color.green;
            quizManager.correct();
        }
        else
        {
            Debug.Log("Wrong Answer");
            GetComponent<Image>().color = Color.red;
            quizManager.incorrect();
        }
    }
}
