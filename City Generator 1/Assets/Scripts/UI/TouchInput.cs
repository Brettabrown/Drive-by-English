using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TouchInput : MonoBehaviour
{
    public enum Directions { Left, Right }

    private static Directions direction;

    public static Directions Direction { get { return direction; } }

    public static bool IsTouching { get { return (Input.touchCount > 0 && !IsTouchingUI); } }

    public static bool IsAccelerating
    {
        get
        {
            return isAccelerating;
        }
    }

    public static bool IsDecelerating { get { return isDecelerating; } }

    private static EventSystem eventSystem;
    private static GraphicRaycaster graphicRaycaster;

    private static bool isAccelerating;

    private static bool isDecelerating;

    [SerializeField]
    private EventTrigger accelerateButtonEventTrigger;

    [SerializeField]
    private EventTrigger decelerateButtonEventTrigger;

    private void Awake()
    {
        eventSystem = GetComponentInParent<EventSystem>();
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();

        AddAccelerateButtonTriggers();
        AddDecelerateButtonTriggers();
    }

    private void AddAccelerateButtonTriggers()
    {
        EventTrigger.Entry onDown = new EventTrigger.Entry();

        onDown.eventID = EventTriggerType.PointerDown;
        onDown.callback.AddListener((e) => isAccelerating = true);

        EventTrigger.Entry onUp = new EventTrigger.Entry();
        onUp.eventID = EventTriggerType.PointerUp;
        onUp.callback.AddListener((e) => isAccelerating = false);

        accelerateButtonEventTrigger.triggers.Add(onDown);
        accelerateButtonEventTrigger.triggers.Add(onUp);
    }

    private void AddDecelerateButtonTriggers()
    {
        EventTrigger.Entry onDown = new EventTrigger.Entry();

        onDown.eventID = EventTriggerType.PointerDown;
        onDown.callback.AddListener((e) => isDecelerating = true);

        EventTrigger.Entry onUp = new EventTrigger.Entry();
        onUp.eventID = EventTriggerType.PointerUp;
        onUp.callback.AddListener((e) => isDecelerating = false);

        decelerateButtonEventTrigger.triggers.Add(onDown);
        decelerateButtonEventTrigger.triggers.Add(onUp);
    }

    private void Update()
    {
        Touch touch = new Touch();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        if(touch.position.x > Screen.width / 2)
        {
            direction = Directions.Right;
        }
        else
        {
            direction = Directions.Left;
        }
        print($"Touch position: {touch.position} screen size: {Screen.width}");
    }

    private static bool IsTouchingUI
    {
        get
        {
            if (Input.touchCount == 0)
            {
                return false;
            }

            PointerEventData data = new PointerEventData(eventSystem);
            data.position = Input.GetTouch(0).position;

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(data, results);
            if(results.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
