using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestionParent : MonoBehaviour
{
    [SerializeField]
    private QuestionSet[] questionSets;

    [SerializeField]
    private TextMeshPro repeatTextPrefab;

    public TextMeshPro RepeatTextPrefab { get { return repeatTextPrefab; } }

    [SerializeField]
    private Color color;

    public Color Color { get { return color; } }

    public QuestionData GetQuestion()
    {
        for(int i = 0; i < questionSets.Length; i++)
        {
            if(questionSets[i].chosenQuestions.Count != 5)
            {
                int random = Random.Range(0, 5);

                while(questionSets[i].chosenQuestions.Contains(random))
                {
                    random = Random.Range(0, 5);
                }

                questionSets[i].chosenQuestions.Add(random);    

                string question = TranslationDictionary.Translate(questionSets[i].english[random], Game.language);
                string correctAnswer = questionSets[i].english[random];
                
                string[] choiceLines = new string[4];
                choiceLines[0] = question;

                List<int> chosen = new List<int>();

                chosen.Add(random);

                int choice= Random.Range(0, 3);

                if (choice == 0)
                {
                    choiceLines[1] = "A." + correctAnswer;

                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }
                    chosen.Add(random);
                    choiceLines[2] = "B. " + questionSets[i].english[random] + "\n";
                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }
                    choiceLines[3] = "C. " + questionSets[i].english[random] + "\n";

                }
                if (choice == 1)
                {
                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }
                    chosen.Add(random);
                    choiceLines[1] = "A. " + questionSets[i].english[random] + "\n";

                    choiceLines[2] = "B." + correctAnswer + "\n";

                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }

                    choiceLines[3] = "C. " + questionSets[i].english[random] + "\n";
                }
                if (choice == 2)
                {
                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }
                    chosen.Add(random);

                    choiceLines[1] = "A. " + questionSets[i].english[random] + "\n";

                    random = Random.Range(0, 5);
                    while (chosen.Contains(random))
                    {
                        random = Random.Range(0, 5);
                    }

                    choiceLines[2] = "B. " + questionSets[i].english[random] + "\n";

                    choiceLines[3] = "C." + correctAnswer + "\n";
                    
                }

                QuestionData questionData = new QuestionData(question, choice, choiceLines, correctAnswer);
                return questionData;
            }
        }

        return new QuestionData();
    }

}

[System.Serializable]
public struct QuestionData
{
    public string question;
    public int correctAnswer;
    public string[] choiceLines;
    public string correctAnswerString;

    public QuestionData(string question, int correctAnswer, string[] choiceLines, string correctAnswerString)
    {
        this.question = question;
        this.correctAnswer = correctAnswer;
        this.choiceLines = choiceLines;
        this.correctAnswerString = correctAnswerString;
    }

}
