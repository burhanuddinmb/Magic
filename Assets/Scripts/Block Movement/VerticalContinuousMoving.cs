using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalContinuousMoving : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] float maxY;
    [SerializeField] float minY;

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
                futurePosition.y += movement;
                if (futurePosition.y >= maxY)
                {
                    futurePosition.y = maxY;
                    isMovingPositive = false;
                    isWaiting = true;
                }
            }
            else
            {
                futurePosition.y -= movement;
                if (futurePosition.y <= minY)
                {
                    futurePosition.y = minY;
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
