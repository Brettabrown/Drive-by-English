using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Question : MonoBehaviour
{
    public static event System.Action ArrivedAtQuestion;
    public static event System.Action<bool> DoneWithQuestion;

    [SerializeField]
    private Text[] text;

    private QuestionParent questionParent;

    private QuestionData questionData;

    private TextMeshPro repeatText;

    public static event System.Action<string> PleaseRepeatTextCreated;

    private void Awake()
    {
        questionParent = GetComponentInParent<QuestionParent>();
    }

    public void Activate()
    {
        ArrivedAtQuestion?.Invoke();

        questionData = questionParent.GetQuestion();

        for(int i = 0; i < 4; i++)
        {
            if(i == questionData.correctAnswer + 1)
            {
                text[i].GetComponent<QuestionTextElement>().isCorrectChoice = true;
            }

            text[i].text = questionData.choiceLines[i];
            //text[i].ForceMeshUpdate();

            //BoxCollider boxCollider = text[i].GetComponent<BoxCollider>();

            //Vector2 textSize = text[i].textBounds.size;

            //boxCollider.size = new Vector2(textSize.x, textSize.y);
        }
    }

    public void CorrectAnswerChosen()
    {
        foreach(Transform transform in transform)
        {
            transform.gameObject.SetActive(false);
        }
        StartCoroutine(RepeatWord(1));
    }

    public void WrongAnswerChosen()
    {
        int count = 0;
        
        foreach(Transform transform in transform)
        {
            if(count == 0)
            {
                count++;
                continue;
            }

            if (count == (questionData.correctAnswer + 1))
            {
                Destroy(transform.parent.GetChild(0).GetComponent<QuestionTextElement>());
            }
            else
            {
                transform.gameObject.SetActive(false);
            }

            count++;
        }
        StartCoroutine(RepeatWord(3));
    }

    private IEnumerator RepeatWord(int times)
    {
        PleaseRepeatTextCreated?.Invoke("Please repeat " + questionData.correctAnswerString + "\n" + times + (times == 1 ? " time." : " times."));

        AudioClip audioClip = AudioManager.GetAudio(questionData.correctAnswerString.Replace(".","").Replace("?", "").Replace("!", ""));

        AudioManager.PlayAudio(audioClip);

        yield return new WaitForSeconds(audioClip.length);

        AudioClip clip;

        bool correct = false;

        if(times == 1)
        {
            correct = true;
        }

        while(times > 0)
        {
            clip = Microphone.Start(Microphone.devices[0], false, 2, 44100);
            yield return new WaitForSeconds(1.5f);
            Microphone.End(Microphone.devices[0]);
            float[] samples = new float[clip.samples * clip.channels];
            clip.GetData(samples, 0);

            float total = 0;

            foreach(float value in samples)
            {
                total += value;
            }

            total = Mathf.Abs(total);

            if(total > 0.14f)
            {
                times--;
                PleaseRepeatTextCreated?.Invoke("Please Repeat " + questionData.correctAnswerString + "\n" + times + (times == 1 ? " time." : " times."));
            }
        }

        DoneWithQuestion?.Invoke(correct);
        PleaseRepeatTextCreated?.Invoke("");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GetComponentInParent<QuestionParent>().Color;
        Gizmos.DrawSphere(transform.position, 7);
    }


}
