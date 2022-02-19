using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowHideButton : MonoBehaviour
{
    private bool isVisible;

    [SerializeField]
    private GameObject minimapObject;

    [SerializeField]
    private GameObject minimapCameraObject;

    private void Awake()
    {
        isVisible = true;
    }

    public void OnPressed()
    {
        isVisible = !isVisible;
        minimapObject.SetActive(isVisible);
        minimapCameraObject.SetActive(isVisible);
    }

}
