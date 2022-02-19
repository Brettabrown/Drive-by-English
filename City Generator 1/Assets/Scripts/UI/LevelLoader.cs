using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;

    public void Load(string levelName)
    {
        Game.language = (TranslationDictionary.Languages)dropdown.value;
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

}
