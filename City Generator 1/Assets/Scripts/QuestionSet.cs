using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionSet
{
    public string[] english = new string[5];

    [HideInInspector]
    public List<int> chosenQuestions;
}
