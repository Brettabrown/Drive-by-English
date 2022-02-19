using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private PlayerCar playerCar;

    private bool hasSwitchedOnAudio;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerCar = GetComponent<PlayerCar>();
    }

    private void Update()
    {
        audioSource.volume = Mathf.Lerp(0, 1, playerCar.Speed / playerCar.MaxSpeed);
    }

}
