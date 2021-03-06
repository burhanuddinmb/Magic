﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Node : MonoBehaviour
{
    // Node starting params
    public bool walkable = true;
    public int gridX;
    public int gridZ;

    //Height
    public int gridY;

    //For Astar
    public float gCost;
    public float hCost;
    public Node parentNode;

    //For blocking
    public bool isOccupied;

    private void Start()
    {
        //gridX = (int)(transform.localPosition.x);
        //gridZ = (int)(transform.localPosition.z);
        //gridY = transform.localPosition.y;

        //if (SceneManager.GetActiveScene().name == "FinalLevel9")
        //{

        //Accomodating scale
        gridX = (int)(transform.localPosition.x);
        gridZ = (int)(transform.localPosition.z);

        gridY = (int)(transform.localPosition.y);

        isOccupied = false;
    }

    public float fcost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public void SetGridX(int xVal)
    {
        gridX = xVal;
    }
    public int GetGridX()
    {
        return gridX;
    }

    public void SetGridZ(int zVal)
    {
        gridY = zVal;
    }

    public int GetGridZ()
    {
        return gridZ;
    }
}