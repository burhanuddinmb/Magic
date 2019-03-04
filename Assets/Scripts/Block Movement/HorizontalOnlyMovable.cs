using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalOnlyMovable : MonoBehaviour
{
    Vector3 futurePosition;
    Node node;
    AccessibleNodes accessibleNodes;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    bool isTouchActive;
    bool isObjectSelected;
    bool deselectObject;

    [SerializeField] float maxX;
    [SerializeField] float minX;

    float startTime;
    float eachFrameTimeVariable;

    float movementSpeed;

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
            futurePosition.x = transform.localPosition.x - (deltaTouchSpace.x * Time.deltaTime * movementSpeed);
            futurePosition.x = Mathf.Clamp(futurePosition.x, minX, maxX);

            transform.localPosition = futurePosition;
            isObjectSelected = !deselectObject;
        }
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
                    if (hit.transform.tag == "HorizontalTouchMove" && hit.transform == transform)
                    {
                        isObjectSelected = true;
                        Camera.main.GetComponent<CameraScript>().isAnythingImportantGoingOn = true;
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
                if (isObjectSelected)
                    Camera.main.GetComponent<CameraScript>().isAnythingImportantGoingOn = false;
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