using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static bool isAtQuestion;

    public static bool IsAtQuestion { get { return isAtQuestion; } }

    private static int score;

    public static int Score { get { return score; } }

    public static TranslationDictionary.Languages language;

    private void Awake()
    {
        Question.ArrivedAtQuestion += OnArrivedAtQuestion;
        Question.DoneWithQuestion += OnDoneWithQuestion;
        score = 0;
    }

    private void OnArrivedAtQuestion()
    {
        isAtQuestion = true;
    }

    private void OnDoneWithQuestion(bool correct)
    {
        isAtQuestion = false;
        if(correct)
        {
            score += 10;
        }
        else
        {
            score += 3;
        }
    }

}
