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
    bool deselectObject;

    [SerializeField] float maxY;
    [SerializeField] float minY;

    float startTime;
    float eachFrameTimeVariable;

    float movementSpeed;

    Node node;
    AccessibleNodes accessibleNodes;

    void Start()
    {
        node = GetComponent<Node>();
        accessibleNodes = GetComponent<AccessibleNodes>();
        movementSpeed = 5.0f;
        deselectObject = true;
    }

    void Update()
    {
        HandleTouch();
        CheckForAccessibleNodes();
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

    void CheckForAccessibleNodes()
    {
        if (Mathf.Abs(transform.localPosition.y - node.gridY) >= 0.5f)
        {
            node.gridY = transform.localPosition.y;
            foreach (var connectingNode in accessibleNodes.connectingNodes)
            {
                connectingNode.transform.GetComponent<AccessibleNodes>().CalculateConnectingNodes();
            }
            accessibleNodes.CalculateConnectingNodes();
            foreach (var connectingNode in accessibleNodes.connectingNodes)
            {
                connectingNode.transform.GetComponent<AccessibleNodes>().CalculateConnectingNodes();
            }
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

    void CheckChangeInTouch(Touch touch)
    {
        if (isObjectSelected)
        {
            deltaTouchSpace = initialTouchSpace - touch.position;
            initialTouchSpace = touch.position;
        }
    }
}