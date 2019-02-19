using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMoving;
    
    public Node currentNode;
    public Node destinationNode;
    List<Node> pathToDestination;

    [SerializeField] float movementSpeed;
    float timer;
    float heldDowntimer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        isMoving = false;
        currentNode = GetComponent<SetPlayerStartingGrid>().startingNode.GetComponent<Node>();
        transform.localPosition = currentNode.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if (isMoving)
        {
            isMoving = MoveToDestination();
        }
            if (currentNode.tag == "VerticalMovers" || currentNode.tag == "HorizontalTouchMove")
            {
                transform.localPosition = currentNode.transform.localPosition;
            }
                
    }

    void CheckInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                heldDowntimer = Time.time;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.tag == "Nodes" || hit.transform.tag == "HorizontalTouchMove" || hit.transform.tag == "VerticalMovers")
                    {
                        destinationNode = hit.transform.GetComponent<Node>();
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                heldDowntimer = Time.time - heldDowntimer;

                if (heldDowntimer < 0.3f && destinationNode)
                {
                    pathToDestination = AllNodes.AStar(currentNode, destinationNode);
                    if (pathToDestination.Count > 0)
                    {
                        isMoving = true;
                    }
                }
                else
                {
                    destinationNode = null;
                }
            }
        }
    }

    bool MoveToDestination()
    {
        timer += Time.deltaTime * movementSpeed;
        transform.localPosition = Vector3.Lerp(currentNode.transform.localPosition, pathToDestination[0].transform.localPosition, timer);

        if (timer >= 1.0f)
        {
            timer = 0.0f;
            transform.localPosition = pathToDestination[0].transform.localPosition;
            currentNode = pathToDestination[0];
            pathToDestination.RemoveAt(0);
        }

        if (pathToDestination.Count == 0)
        {
            destinationNode = null;
            return false;
        }

        return true;
    }
}