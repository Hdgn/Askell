//Question and Answer Input
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//System.Serializable allows this to be called throughout the game
//Introduces the structure of each question including the question text, list of answers, and which answer in the list of answers is 
//correct.
[System.Serializable]
public class QuestionAndAnswers
{
    [TextArea]
    public string Question;         //questions text
    public string[] Answers;        //answers to select
    public int CorrectAnswer;        //correct answer (use Element + 1)
}

//System.Serializable allows this to be called throughout the game
//Introduces the two different level types within the game.
[System.Serializable]
public enum LevelType
{
    QUIZ,
    DRAG
}
