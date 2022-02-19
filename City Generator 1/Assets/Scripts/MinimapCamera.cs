using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float yDistance;

    private void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, yDistance, playerTransform.position.z);
        transform.localEulerAngles = new Vector3(90, playerTransform.localEulerAngles.y, 0);
    }


}
