using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject endingPoint;

    [SerializeField] List<Node> pathToTake;

    float timer;
    [SerializeField] float movementSpeed = 2.0f;

    int direction = 1;
    int currentIndex;
    int nextIndex;

    bool isMoving;

    [SerializeField] bool setCustomPath;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        nextIndex = 1;
        isMoving = true;
        timer = 0.0f;

        if (!setCustomPath)
        {
            pathToTake = AllNodes.AStar(startingPoint.GetComponent<Node>(), endingPoint.GetComponent<Node>());
        }
        pathToTake[nextIndex].isOccupied = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
        else
        {
            isMoving = CheckForBlockToFreeUp();
        }
    }

    void Move()
    {
        timer += Time.deltaTime * movementSpeed;
        transform.localPosition = Vector3.Lerp(pathToTake[currentIndex].transform.localPosition, pathToTake[nextIndex].transform.localPosition, timer);

        if (timer >= 1.0f)
        {
            timer = 0.0f;
            transform.localPosition = pathToTake[nextIndex].transform.localPosition;

            currentIndex = nextIndex;
            nextIndex += direction;
            if (nextIndex >= pathToTake.Count)
            {
                direction = -1;
                nextIndex -= 2;
            }
            else if (nextIndex < 0)
            {
                direction = 1;
                nextIndex += 2;
            }
            BlockModifications();
        }
    }

    void BlockModifications()
    {
        if (pathToTake[nextIndex].isOccupied)
        {
            isMoving = false;
        }
        else
        {
            pathToTake[currentIndex].isOccupied = false;
            pathToTake[nextIndex].isOccupied = true;
        }
    }

    bool CheckForBlockToFreeUp()
    {
        if (pathToTake[nextIndex].isOccupied)
        {
            return false;
        }
        pathToTake[currentIndex].isOccupied = false;
        pathToTake[nextIndex].isOccupied = true;
        return true;
    }
}
