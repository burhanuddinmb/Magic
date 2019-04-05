using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class HorizontalOnlyZMovable : MonoBehaviour
{
    Vector3 futurePosition;
    Node node;
    AccessibleNodes accessibleNodes;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    bool isTouchActive;
    bool isObjectSelected;
    bool deselectObject;
    [Tooltip("Positive or negative direction depending on the touch. True being moving in world is in positive with the screen space")]
    [SerializeField] bool polarity;

    [SerializeField] float maxZ;
    [SerializeField] float minZ;

    float startTime;
    float eachFrameTimeVariable;

    [SerializeField] float movementSpeed = 2.0f;
    List<PlayerMovement> players;


    void Start()
    {
        node = GetComponent<Node>();
        accessibleNodes = GetComponent<AccessibleNodes>();
        deselectObject = true;

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        players = new List<PlayerMovement>();
        foreach (var item in playerObjects)
        {
            players.Add(item.GetComponent<PlayerMovement>());
        }

        if (polarity)
        {
            movementSpeed *= -1.0f;
        }
    }

    void Update()
    {
        HandleTouch();
        CheckForAccessibleNodes();

        foreach (var player in players)
        {
            if (player.isMoving && (node.isOccupied || player.currentNode == node))
            {
                //Return as we would not want the player to be able to move critical blocks during gameplay
                return;
            }
        }

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;

            futurePosition = transform.localPosition;
            futurePosition.z = transform.localPosition.z + (deltaTouchSpace.x * Time.deltaTime * movementSpeed);
            futurePosition.z = Mathf.Clamp(futurePosition.z, minZ, maxZ);

            transform.localPosition = futurePosition;
            isObjectSelected = !deselectObject;
        }
    }

    void CheckForAccessibleNodes()
    {
        if (Mathf.Abs(transform.localPosition.z - node.gridZ) >= 1)
        {
            node.gridZ = Mathf.RoundToInt(transform.localPosition.z);
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
                }
            }
        }
    }

    void CheckChangeInTouch(Touch touch)
    {
        if (isObjectSelected)
        {
            deltaTouchSpace = initialTouchSpace - touch.position;
            deltaTouchSpace.x *= 1080.0f / Screen.width;
            deltaTouchSpace.y *= 1920.0f / Screen.height;
            initialTouchSpace = touch.position;
        }
    }
}