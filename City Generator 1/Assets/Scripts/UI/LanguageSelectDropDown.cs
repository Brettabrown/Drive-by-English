using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LanguageSelectDropDown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        string[] names = System.Enum.GetNames(typeof(TranslationDictionary.Languages));

        dropdown.AddOptions(new List<string>(names));

        dropdown.value = (int)TranslationDictionary.Languages.Spanish;
    }
}
