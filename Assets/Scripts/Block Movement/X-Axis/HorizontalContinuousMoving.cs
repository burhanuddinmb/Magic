using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HorizontalContinuousMoving : MonoBehaviour
{
    [SerializeField] float maxX;
    [SerializeField] float minX;

    Vector3 futurePosition;
    Node node;

    float movementSpeed;
    [SerializeField] float timeToWait;
    float timeTracker;

    [SerializeField] bool isMovingPositive;
    bool isWaiting;
    AccessibleNodes accessibleNodes;

    void Start()
    {
        node = GetComponent<Node>();
        accessibleNodes = GetComponent<AccessibleNodes>();
        movementSpeed = 5.0f;
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
        CheckForAccessibleNodes();
    }

    void CheckForAccessibleNodes()
    {
        if (Mathf.Abs(transform.localPosition.x - node.gridX) >= 1)
        {
            node.gridX = Mathf.RoundToInt(transform.localPosition.x);
            ReAdjustNodes();
        }
    }

    void ReAdjustNodes()
    {
        foreach (var connectingNode in accessibleNodes.connectingNodes)
        {
            AccessibleNodes accNodes = connectingNode.transform.GetComponent<AccessibleNodes>();
            for (int i = 0; i < accNodes.connectingNodes.Count; i++)
            {
                accNodes.connectingNodes.Remove(transform.GetComponent<Node>());
            }
        }
        accessibleNodes.CalculateConnectingNodes();

        foreach (var connectingNode in accessibleNodes.connectingNodes)
        {
            connectingNode.transform.GetComponent<AccessibleNodes>().connectingNodes.Add(transform.GetComponent<Node>());
        }
    }
}
