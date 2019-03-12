using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class VerticalContinuousMoving : MonoBehaviour
{
    [SerializeField] float maxY;
    [SerializeField] float minY;

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
        CheckForAccessibleNodes();
    }

    void CheckForAccessibleNodes()
    {
        if (Mathf.Abs(transform.localPosition.y - node.gridY) >= 1)
        {
            node.gridY = Mathf.RoundToInt(transform.localPosition.y);
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
