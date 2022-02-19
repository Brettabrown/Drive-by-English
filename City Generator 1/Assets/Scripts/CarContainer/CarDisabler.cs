using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDisabler : MonoBehaviour
{
    public Transform playerTransform;

    public float maxDistance;

    private bool hasBeenDisabled;

    private TrafficCar trafficCar;

    private void Awake()
    {
        trafficCar = GetComponent<TrafficCar>();
    }

    private void Start()
    {
        StartCoroutine(DistanceCheck());
    }

    private IEnumerator DistanceCheck()
    {
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return null;
            }

            if (Vector3.Distance(transform.position, playerTransform.position) >= maxDistance)
            {
                if (!hasBeenDisabled)
                {
                    hasBeenDisabled = true;
                    Disable();
                }
            }
            else
            {
                if(hasBeenDisabled)
                {
                    hasBeenDisabled = false;
                    Enable();
                }
            }
        }
    }

    private void Enable()
    {
        foreach(Transform transform in transform)
        {
            transform.gameObject.SetActive(true);
        }
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = Vector2.zero;
        trafficCar.enabled = true;

        trafficCar.InvokeRepeating(nameof(trafficCar.MoveCar), 0.02f, 0.02f);
    }

    private void Disable()
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(false);
        }
        GetComponent<Rigidbody>().isKinematic = true;
        trafficCar.enabled = false;
        trafficCar.CancelInvoke();
    }

}
