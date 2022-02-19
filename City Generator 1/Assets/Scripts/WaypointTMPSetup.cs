using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaypointTMPSetup : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPointParents;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform tmpPrefab;

    [SerializeField]
    private Transform waypointTextParent;

    private void Start()
    {
        foreach(Transform parent in waypointTextParent)
        {
            foreach(Transform text in parent)
            {
                text.gameObject.SetActive(false);
            }
        }
    }
}
