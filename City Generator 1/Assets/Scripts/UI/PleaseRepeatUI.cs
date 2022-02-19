using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PleaseRepeatUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Question.PleaseRepeatTextCreated += OnPleaseRepeatCreated;   
    }

    private void OnDisable()
    {
        Question.PleaseRepeatTextCreated -= OnPleaseRepeatCreated;
    }

    private void OnPleaseRepeatCreated(string text)
    {
        this.text.text = text;
    }

}
