using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    AccessibleNodes accessibleNodes;
    Node node;

    bool isThisElevatorSelected;
    bool checkIfSelected;

    [SerializeField] float movementSpeed;
    [SerializeField] float topY;
    [SerializeField] float bottomY;
    float heldDownTimer;

    Vector3 deltaTouchSpace;
    Vector3 initialTouchSpace;
    Vector3 futurePosition;

    // Start is called before the first frame update
    void Start()
    {
        accessibleNodes = GetComponent<AccessibleNodes>();
        node = GetComponent<Node>();
        isThisElevatorSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAccessibleNodes();
        CheckForTouch();
        if (isThisElevatorSelected)
        {
            CalculateMovement();
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

    void CheckForTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            heldDownTimer = Time.time;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.tag == "Elevator" || hit.transform == this.transform)
                {
                    checkIfSelected = true;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (checkIfSelected && Time.time - heldDownTimer > 0.3f && !isThisElevatorSelected)
            { 
                isThisElevatorSelected = true;
                checkIfSelected = false;
            }

            if (isThisElevatorSelected)
            {
                CheckChangeInMovement();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isThisElevatorSelected = false;
            checkIfSelected = false;
        }
    }

    void CalculateMovement()
    {
        futurePosition = transform.localPosition;
        futurePosition.y = transform.localPosition.y - (deltaTouchSpace.y * Time.deltaTime * movementSpeed);
        futurePosition.y = Mathf.Clamp(futurePosition.y, bottomY, topY);

        transform.localPosition = futurePosition;
    }



    void CheckChangeInMovement()
    {
        if (isThisElevatorSelected)
        {
            deltaTouchSpace = initialTouchSpace - Input.mousePosition;
            initialTouchSpace = Input.mousePosition;
        }
    }
}