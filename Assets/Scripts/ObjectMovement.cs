using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    Vector3 futurePosition;
    Vector3 frameStartPosition;
    Vector3 forceToBeAdded;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    bool isTouchActive;
    bool isObjectSelected;
    bool isPlayerConnected;
    bool waitForForce;
    bool applyForce;
    bool deselectObject;
    bool resetValues;

    [SerializeField] GameObject player;

    [SerializeField] float maxY;
    [SerializeField] float minY;

    float startTime;
    float eachFrameTimeVariable;

    float movementSpeed;
    [SerializeField]
    private float thrust;

    void Start()
    {
        movementSpeed = 2.0f;
        //thrust = 1.8f;
        isPlayerConnected = false;
        forceToBeAdded = Vector3.zero;
        deselectObject = true;
        resetValues = false;
    }

    void Update()
    {
        HandleTouch();

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;

            futurePosition = transform.position;
            futurePosition.y = transform.position.y - (deltaTouchSpace.y * Time.deltaTime * movementSpeed);
            futurePosition.y = Mathf.Clamp(futurePosition.y, minY, maxY);

            if (resetValues)
            {
                frameStartPosition = futurePosition;
                eachFrameTimeVariable = Time.time;
                resetValues = false;
            }
            if (futurePosition.y == maxY || applyForce)
            {
                CalculateForceToApply();
                waitForForce = false;
                applyForce = false;
                frameStartPosition = futurePosition;
            }

            transform.position = futurePosition;
            ApplyForce();

            isObjectSelected = !deselectObject;
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.tag == "VerticalMovers" && hit.transform == transform)
                    {
                        isObjectSelected = true;
                    }
                }
                startTime = Time.time;
                eachFrameTimeVariable = startTime;
                initialTouchSpace = touch.position;
                frameStartPosition = transform.position;
                waitForForce = false;
                applyForce = false;
                deselectObject = false;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                CheckChangeInTouch(touch);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                CheckChangeInTouch(touch);
                deselectObject = true;
            }

            if (!isTouchActive && isObjectSelected)
            {
                float timeChange = Time.time - startTime;

                if (timeChange > 0.1f)
                {
                    isTouchActive = true;
                    //player.GetComponent<PlayerController>().StopPlayer();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
            isPlayerConnected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform.parent;
            isPlayerConnected = false;
        }
    }

    void ApplyForce()
    {
        if (isPlayerConnected && forceToBeAdded.y < -0.1f)
        {
            //Debug.Log("Force: " + forceToBeAdded);
            player.GetComponent<Rigidbody>().AddForce(-forceToBeAdded);
            forceToBeAdded = Vector3.zero;
        }
    }

    void CheckChangeInTouch(Touch touch)
    {
        if (isObjectSelected)
        {
            deltaTouchSpace = initialTouchSpace - touch.position;
            initialTouchSpace = touch.position;
            if (deltaTouchSpace.y < 0)
            {
                waitForForce = true;
            }
            else
            {
                if (waitForForce == true)
                {
                    applyForce = true;
                }
                else
                {
                    //Calculating values just before player starts pushing it up
                    resetValues = true;
                }
            }
        }
    }

    void CalculateForceToApply()
    {
        if (isPlayerConnected)
        {
            float timeDiff = Time.time - eachFrameTimeVariable;
            eachFrameTimeVariable = Time.time;

            float deltaChangeInSpace = frameStartPosition.y - futurePosition.y;
            frameStartPosition = futurePosition;

            forceToBeAdded = Vector3.up * thrust * deltaChangeInSpace / timeDiff;
            //Debug.Log("Time diff: " + timeDiff);
            //Debug.Log("Position diff: " + deltaChangeInSpace);
        }
    }
}