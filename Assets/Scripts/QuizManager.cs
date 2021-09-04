//Quiz Manager Input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    //Creates a list of Questions using the Questions and Answers script which details how each question is designed.
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public static bool GameIsOver;
    [SerializeField] public GameManager gameManager;
    [SerializeField] private float timeDelay = 1f;

    [SerializeField] private Text QuestionTxt;
    [SerializeField] private List<Image> livesLeft;
    public Text ScoreTxt;
    public Text winScoreTxt;
    private int livesRemaining = 3;

    public float totalQuestions = 0;
    public int Score;

    public void Start()
    {
        totalQuestions = QnA.Count;
        livesRemaining = 3;
        randomiseQuestion();
    }

    //Called when the player corrects answers a question it adds to the score and removes the question from the question list
    public void correct()
    {
        AudioManager.instance.PlaySound("Correct");
        Score += 1;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(QuestionTransition());
    }

    //Called when the player incorrects answers a question it removes a life and removes the question from the question list. 
    //It also checkes wheather the removal of a life means there are no more left; if so the game ends.
    public void incorrect()
    {
        AudioManager.instance.PlaySound("Incorrect");
        livesRemaining--;
        LifeLost(livesRemaining);
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(QuestionTransition());

        if (livesRemaining <= 0)
        {
            gameManager.EndGame();
        }
    }

    //The lives sprite is initially coloured red, this removes that for the lost life and it is white instead
    //to indicate to the user that the life has been lost.
    public void LifeLost(int lives)
    {
        livesLeft[lives].color = Color.white;
    }

    //This class is used to assign the answer written in the QnA list to the buttons within the game.
    //Set as their own array the number c=of buttons can been edieted depending on the number of answers to the question. 
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().initialColour;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }

    }

    //The class randomises the order of the questions within the QnA list after each question as long as there are questions remaining.
    //It then assigns the variable current question a question and sets this to the question text within the game. This variable is
    //also used when assigning the answers. If there are no questions left the gameManager is called to determing the score and therefore
    //the level UI/
    void randomiseQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            gameManager.EndGame();
        }
    }

    //This delays the next question by a second allowing the user to see the colour change of the question and hear the audio which
    //indicaes to the player whether they answers correctly or incorrectly.
    IEnumerator QuestionTransition()
    {
        yield return new WaitForSeconds(timeDelay);
        randomiseQuestion();
    }
}
