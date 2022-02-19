using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDistanceDetector : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPointParents;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform waypointTextParentParent;

    [SerializeField]
    private float questionDistanceThreshold;

    public static event System.Action<float> ArrivedAtQuestion;

    private void Update()
    {
        if(!Game.IsAtQuestion)
        {
            GetNearestQuestion();
        }
    }

    private void GetNearestQuestion()
    {
        float minimum = Mathf.Infinity;
        Transform minimumText = null;

        foreach(Transform parent in waypointTextParentParent)
        {
            foreach(Transform text in parent) 
            {
                float distance = Vector3.Distance(player.position, text.position);
                if (distance < minimum)
                {
                    minimum = distance;
                    minimumText = text;
                }
            }
        }

        if(!(minimum <= questionDistanceThreshold))
        {
            return;
        }

        minimumText.gameObject.SetActive(true);

        float deltaX = minimumText.position.x - player.position.x;
        float deltaZ = minimumText.position.z - player.position.z;

        minimumText.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(deltaX, deltaZ) * Mathf.Rad2Deg, 0));
        minimumText.GetComponent<Question>().Activate();
        ArrivedAtQuestion?.Invoke(Mathf.Atan2(deltaX, deltaZ) * Mathf.Rad2Deg);

    }

}
