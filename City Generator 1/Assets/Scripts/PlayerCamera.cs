using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCamera : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private LayerMask questionLayer;

    public static event System.Action RightQuestionClicked;

    public static event System.Action WrongQuestionClicked;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 40, questionLayer))
            {
                if (hit.transform.GetComponent<QuestionTextElement>().isCorrectChoice)
                {
                    hit.transform.GetComponentInParent<Question>().CorrectAnswerChosen();
                    //right say once and drive off
                }
                else if (!hit.transform.GetComponent<QuestionTextElement>().isCorrectChoice)
                {
                    hit.transform.GetComponentInParent<Question>().WrongAnswerChosen();
                    //wrong shows right and say it 3 times
                    WrongQuestionClicked?.Invoke();
                }
            }
        }
    }
}
