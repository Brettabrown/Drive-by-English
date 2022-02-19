using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class TranslationDictionary
{
    public enum Languages
    {
        Arabic,
        Bangla,
        Chinese,
        French,
        HaitianCreole,
        Malay,
        Portuguese,
        Russian,
        Spanish,
        Swahili,
        Tamil,
        Thai,
        Urdu
    };

    private static Dictionary<string, TranslationSentenceInfo> sentences;

    public static string Translate(string sentence, Languages language)
    {
        if (sentences == null)
        {
            Setup();
        }

        Debug.Log(sentence);

        return sentences[sentence].translations[(int)language];
    }

    private static void Setup()
    {
        sentences = new Dictionary<string, TranslationSentenceInfo>();

        TextAsset file = (TextAsset)Resources.Load("Text/Translations", typeof(TextAsset));

        StringReader stringReader = new StringReader(file.text);

        while (true)
        {
            string text = stringReader.ReadLine();

            if (text == null)
            {
                break;
            }

            string[] data = text.Split('	');

            if (data.Length != System.Enum.GetNames(typeof(Languages)).Length + 1)
            {
                continue;
            }

            string title = data[0];

            string[] _sentences = new string[System.Enum.GetNames(typeof(Languages)).Length];

            for(int i = 0; i < System.Enum.GetNames(typeof(Languages)).Length; i++)
            {
                _sentences[i] = data[i + 1];
            }

            TranslationSentenceInfo info = new TranslationSentenceInfo(_sentences);

            if (!sentences.ContainsKey(title))
            {
                sentences.Add(title, info);
            }
        }
    }

}

public struct TranslationSentenceInfo
{
    public string[] translations;

    public TranslationSentenceInfo(string[] translations)
    {
        this.translations = translations;
    }

}
