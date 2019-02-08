using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalOnlyMovable : MonoBehaviour
{
    Vector3 futurePosition;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    bool isTouchActive;
    bool isObjectSelected;
    bool isPlayerConnected;
    bool deselectObject;

    [SerializeField] GameObject player;

    [SerializeField] float maxY;
    [SerializeField] float minY;

    float startTime;
    float eachFrameTimeVariable;

    float movementSpeed;

    void Start()
    {
        movementSpeed = 5.0f;
        isPlayerConnected = false;
        deselectObject = true;
    }

    void Update()
    {
        HandleTouch();

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;

            futurePosition = transform.localPosition;
            futurePosition.y = transform.localPosition.y - (deltaTouchSpace.y * Time.deltaTime * movementSpeed);
            futurePosition.y = Mathf.Clamp(futurePosition.y, minY, maxY);

            transform.localPosition = futurePosition;
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

    void CheckChangeInTouch(Touch touch)
    {
        if (isObjectSelected)
        {
            deltaTouchSpace = initialTouchSpace - touch.position;
            initialTouchSpace = touch.position;
        }
    }
}
