using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalContinuousMoving : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float maxX;
    [SerializeField] float minX;

    float movementSpeed;
    [SerializeField] float timeToWait;
    float timeTracker;

    bool isPlayerConnected;
    [SerializeField] bool isMovingPositive;
    bool isWaiting;

    Vector3 futurePosition;

    void Start()
    {
        movementSpeed = 5.0f;
        isPlayerConnected = false;
        isWaiting = true;
    }

    void Update()
    {
        float movement = Time.deltaTime * movementSpeed;
        futurePosition = transform.localPosition;
        if (isWaiting)
        {
            timeTracker += Time.deltaTime;
            if (timeTracker >= timeToWait)
            {
                isWaiting = false;
                timeTracker = 0.0f;
            }
        }
        else
        {
            if (isMovingPositive)
            {
                futurePosition.x += movement;
                if (futurePosition.x >= maxX)
                {
                    futurePosition.x = maxX;
                    isMovingPositive = false;
                    isWaiting = true;
                }
            }
            else
            {
                futurePosition.x -= movement;
                if (futurePosition.x <= minX)
                {
                    futurePosition.x = minX;
                    isMovingPositive = true;
                    isWaiting = true;
                }
            }
        }
        transform.localPosition = futurePosition;
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
}
