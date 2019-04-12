using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    public bool isMoving;
    public bool isSelected = true;

    public Node currentNode;
    public Node destinationNode;

    List<Node> pathToDestination;

    [SerializeField] float movementSpeed;
    float timer;
    float heldDowntimer;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        timer = 0.0f;
        isMoving = false;
        pathToDestination = new List<Node>();
        currentNode = GetComponent<SetPlayerStartingGrid>().startingNode.GetComponent<Node>();
        currentNode.isOccupied = true;
        transform.localPosition = currentNode.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            CheckInput();
        }
        if (isMoving)
        {
            isMoving = MoveToDestination();
        }
        if (currentNode.tag == "VerticalMovers" || currentNode.tag == "HorizontalTouchMove")
        {
            if (!isMoving)
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
                        //transform.GetChild(0).GetComponent<Highlight>().isVisible = false;
                        destinationNode = hit.transform.GetComponent<Node>();
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                heldDowntimer = Time.time - heldDowntimer;

                if (heldDowntimer < 0.3f && destinationNode)
                {
                    List<Node> tempPath = AllNodes.AStar(currentNode, destinationNode);

                    //Edge cases
                    if (tempPath.Count > 0 && tempPath[0].isOccupied)
                    {
                        if (isMoving)
                        {
                            //Occupancy set by the player
                            if (tempPath[0] != pathToDestination[0])
                            {
                                Node onlyRemainingNode = pathToDestination[0];
                                pathToDestination.Clear();
                                pathToDestination.Add(onlyRemainingNode);
                                return;
                            }
                        }
                        else
                        {
                            destinationNode = null;
                            return;
                        }
                    }

                    //If already moving, adjust
                    if (pathToDestination.Count > 0)
                    {
                        if (pathToDestination[0] != tempPath[0])
                        {
                            tempPath.Insert(0, currentNode);
                            tempPath.Insert(0, pathToDestination[0]);
                        }
                    }

                    //Set occupancy
                    pathToDestination = tempPath;
                    if (pathToDestination.Count > 0)
                    {
                        if (!isMoving)
                        {
                            currentNode.isOccupied = false;
                            isMoving = true;
                        }
                        pathToDestination[0].isOccupied = true;
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
            BlockModifications();
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

    void BlockModifications()
    {
        if (pathToDestination.Count > 1)
        {
            if (pathToDestination[1].isOccupied)
            {
                Node onlyRemainingNode = pathToDestination[0];
                pathToDestination.Clear();
                pathToDestination.Add(onlyRemainingNode);
                return;
            }
            if (!pathToDestination[1].transform.GetComponent<AccessibleNodes>().connectingNodes.Contains(pathToDestination[0]))
            {
                Node onlyRemainingNode = pathToDestination[0];
                pathToDestination.Clear();
                pathToDestination.Add(onlyRemainingNode);
            }
            else
            {
                pathToDestination[0].isOccupied = false;
                pathToDestination[1].isOccupied = true;
            }
        }
    }
}