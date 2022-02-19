using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarContainer : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float maxDistance;

    private void Awake()
    {
        foreach(Transform transform in transform)
        {
            CarDisabler disabler = (CarDisabler)transform.gameObject.AddComponent(typeof(CarDisabler));

            disabler.playerTransform = playerTransform;
            disabler.maxDistance = maxDistance;
        }
    }
}