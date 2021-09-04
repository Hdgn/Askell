//Drag_DropManager Input
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Drag_DropManager : MonoBehaviour
{
    private static int totalCount = 4;
    public int totalQuestions = 4;
    public Text ScoreTxt;
    public Text winScoreTxt;
    [SerializeField] public GameManager gameManager;

    private bool done;

    private Vector2[] positions = new Vector2[totalCount];
    public int Score;

    //Used to store the Answers, the Questions, and The Answer boxes.
    public GameObject[] answers = new GameObject[totalCount];
    public GameObject[] boxes = new GameObject[totalCount];
    public GameObject[] statements = new GameObject[totalCount];
   
    //Public strings means that they may be updated within the levels.
    public string statement1 = "Hello";
    public string statement2 = "Hello";
    public string statement3 = "Hello";
    public string statement4 = "Hello";


    void Start()
    {
        SetPositions();
        AssignStatements();
    }

    void Update()
    {
        CheckChildCount();
    }

    //Creates a randomised list with the length of the nuber of statements.
    public List<int> GenerateRandomList()
    {
        List<int> list = new List<int>();
        int rand;
        for (int i = 0; i < totalCount; i++)
        {
            rand = Random.Range(0, totalCount);

            while (list.Contains(rand))
            {
                rand = Random.Range(0, totalCount);
            }

            list.Add(rand);
        }

        return list;
    }

    void SetPositions()
    {   
        //Used to ransomised the position of the statements, before assigning the corresponding ansswer to the same position order.
        List<int> randomList = GenerateRandomList();
        for (int i = 0; i < totalCount; i++)
        {
            positions[i] = statements[i].GetComponent<RectTransform>().transform.position;
        }
        for (int i = 0; i < totalCount; i++)
        {
            statements[i].GetComponent<RectTransform>().transform.position = positions[randomList[i]];
        }
        for (int i = 0; i < totalCount; i++)
        {
            positions[i] = boxes[i].GetComponent<RectTransform>().transform.position;
        }
        for (int i = 0; i < totalCount; i++)
        {
            boxes[i].GetComponent<RectTransform>().transform.position = positions[randomList[i]];
        }
    }

    //Assigns the public strings to the the statement area
    //originally contained another switch and case with the statements randomly assigned to a level,
    //however, issues arose during level implementation so this was removed.
    void AssignStatements()
    {
        for (int i = 0; i < totalCount; i++)
        {
            string statementText = "";

            switch (i)
            {
                case 0:
                    statementText = statement1;
                    break;

                case 1:
                    statementText = statement2;
                    break; 

                case 2:
                    statementText = statement3;
                    break;

                case 3:
                    statementText = statement4;
                    break;
            }
            statements[i].GetComponent<TMPro.TextMeshProUGUI>().text = statementText;
        }
    }

    //Checks the Answer and Answer boxes to determine if they each have a single answer
    //If each answer is assigned to a box by the user the game finishes the game checking the score.
    void CheckChildCount()
    {
        int childrenCount = 0;

        for (int i = 0; i < totalCount; i++)
        {
            if (boxes[i].transform.childCount > 0)
            {
                childrenCount++;
            }
        }
        if (childrenCount == totalCount)
        {
            done = true;
            CheckScore();
        }
    }


    // The score is calculated depending whether the answer's parent is equal to it's asigned box answer
    // gameManager.EngGame is then called to assign the correct UI output and finish the level. 
    void CheckScore()
    {
        Score = 0;
        for (int i = 0; i < totalCount; i++)
        {
            if (answers[i].transform.parent == boxes[i].transform)
            {
                Score++;
            }
        }

        Debug.Log("All answers placed");
        gameManager.EndGame();
    }

}