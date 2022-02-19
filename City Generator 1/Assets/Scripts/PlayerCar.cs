using System.Collections;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField]
    private float acceleration;

    private float speed;

    [SerializeField]
    private float sensitivity;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private Transform frontWheelTransformRight;

    [SerializeField]
    private Transform frontWheelTransformLeft;

    private float wheelDirection;

    public float Speed { get { return speed; } }

    public float MaxSpeed { get { return maxSpeed; } }

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        WaypointDistanceDetector.ArrivedAtQuestion += OnArrivedAtQuestion;
        Question.DoneWithQuestion += OnDoneWithQuestion;
    }

    private void OnDisable()
    {
        WaypointDistanceDetector.ArrivedAtQuestion -= OnArrivedAtQuestion;
        Question.DoneWithQuestion -= OnDoneWithQuestion;
    }

    private void OnArrivedAtQuestion(float angle)
    {
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
        StartCoroutine("Smooth", angle);
    }

    private void OnDoneWithQuestion(bool correct)
    {
        StopCoroutine("Smooth");
    }

    private IEnumerator Smooth(float angle)
    {
        if (angle < 0)
        {
            angle = 360 + angle;
        }

        while (Mathf.Abs(transform.localEulerAngles.z - angle) > 1f)
        {
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                Mathf.MoveTowardsAngle((transform.localEulerAngles.y < 0 ? 360 + transform.localEulerAngles.y : transform.localEulerAngles.y), angle, maxSpeed / 2f),
                transform.localEulerAngles.z
                );
            yield return null;
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
    }


    private IEnumerator SmoothTowardsPosition(Vector3 position)
    {
        float angle = Mathf.Infinity;

        float yAngle = transform.localEulerAngles.y;

        while (Mathf.Abs(yAngle - angle) > 1f)
        {
            angle = Mathf.Atan2((transform.position.y - position.y), (transform.position.x - position.x)) * Mathf.Rad2Deg;

            angle = -90 - angle;



            if (angle < 0)
            {
                angle += 360;
            }

            yAngle = transform.localEulerAngles.y;

            if (yAngle < 0)
            {
                yAngle += 360;
            }

            print($"{angle - yAngle}");

            float newAngle = Mathf.MoveTowardsAngle(yAngle, angle, maxSpeed * Time.deltaTime);

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newAngle, transform.localEulerAngles.z);

            transform.position += transform.forward * 2 * Time.deltaTime;

            yield return null;
        }
    }

    [SerializeField]
    private float distance;

    private bool wasMovingForward;

    private void Update()
    {
        wheelDirection = 0;
        if (!Game.IsAtQuestion)
        {
            if (Input.GetKey(KeyCode.A) || (TouchInput.IsTouching && TouchInput.Direction == TouchInput.Directions.Left))
            {
                wheelDirection = -speed / 2f;
            }
            if (Input.GetKey(KeyCode.D) || (TouchInput.IsTouching && TouchInput.Direction == TouchInput.Directions.Right))
            {
                wheelDirection = speed / 2f;
            }
        }

        if (((Input.GetKey(KeyCode.W) || TouchInput.IsAccelerating) || (Input.GetKey(KeyCode.S) || TouchInput.IsDecelerating)) && !Game.IsAtQuestion)
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            speed -= acceleration * Time.deltaTime * 2;
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        transform.localEulerAngles += Vector3.up * wheelDirection * Time.deltaTime * speed;

        if ((Input.GetKey(KeyCode.W) || TouchInput.IsAccelerating))
        {
            wasMovingForward = true;
        }
        if ((Input.GetKey(KeyCode.S) || TouchInput.IsDecelerating))
        {
            wasMovingForward = false;
        }


        //Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad)), Color.red);
        //Debug.DrawRay(transform.position + Vector3.up * 0.5f + transform.forward * distance, transform.forward * speed * Time.deltaTime, Color.red);
        //print(transform.forward);

        Ray ray = new Ray(transform.position + Vector3.up * 0.7f + transform.forward * distance, transform.forward);

        RaycastHit hit;

        if (wasMovingForward)
        {
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime + 1.3f) && hit.transform.name != "Stop")
            {
                speed = 0;
            }
            else
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }

        ray = new Ray(transform.position + Vector3.up * 0.7f - transform.forward * distance, -transform.forward);

        if (!wasMovingForward)
        {
            if (Physics.Raycast(ray, out hit, speed * Time.deltaTime + 1.3f) && hit.transform.name != "Stop")
            { 
                speed = 0;
            }
            else
            {
                transform.position -= transform.forward * speed * Time.deltaTime;
            }
        }



    }

}
