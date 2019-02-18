using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Node starting params
    public bool walkable = true;
    public int gridX;
    public int gridZ;

    //Height
    public float gridY;

    //For Astar
    public float gCost;
    public float hCost;
    public Node parentNode;

    private void Start()
    {
        gridX = (int)(transform.localPosition.x + 0.5f);
        gridZ = (int)(transform.localPosition.z + 0.5f);
        gridY = transform.localPosition.y;
    }

    public float fcost
    {
        get
        {
            return gCost + hCost;
        }
    }
}