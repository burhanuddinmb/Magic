using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 futurePosition;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    bool isTouchActive;
    bool isObjectSelected;

    [SerializeField] bool isVertical;

    [SerializeField] GameObject player;

    [SerializeField] float maxY;
    [SerializeField] float minY;

    float maximumY;
    float minimumY;

    float startTime;
    float movementSpeed;

    void Start()
    {
        movementSpeed = 1.0f;
        startPosition = transform.position;
        maximumY = startPosition.y + maxY;
        minimumY = startPosition.y - minY;
    }

    void Update()
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
                    if (hit.transform.parent == transform)
                    {
                        isObjectSelected = true;
                    }
                }
                startTime = Time.time;
                initialTouchSpace = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (isObjectSelected)
                {
                    deltaTouchSpace = initialTouchSpace - touch.position;
                    initialTouchSpace = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isObjectSelected = false;
            }

            if (!isTouchActive && isObjectSelected)
            {
                float timeChange = Time.time - startTime;

                if (timeChange > 0.1f)
                {
                    isTouchActive = true;
                    player.GetComponent<PlayerController>().StopPlayer();
                }
            }
        }

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;

            if (isVertical)
            {
                futurePosition = transform.position;
                futurePosition.y = transform.position.y - (deltaTouchSpace.y * Time.deltaTime * movementSpeed);
                if (futurePosition.y > maximumY)
                {
                    futurePosition.y = maximumY;
                }
                else if (futurePosition.y < minimumY)
                {
                    futurePosition.y = minimumY;
                }
                transform.position = futurePosition;
            }
        }
    }
}