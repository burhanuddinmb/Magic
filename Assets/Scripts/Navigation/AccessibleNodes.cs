﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AccessibleNodes : MonoBehaviour
{
    public List<Node> connectingNodes;

    private void Start()
    {
        CalculateConnectingNodes();
    }

    public void CalculateConnectingNodes()
    {
        Node currentNode = GetComponent<Node>();

        if (connectingNodes.Count >0)
        {
            connectingNodes.Clear();
        }
        for (int i = 0; i < AllNodes.allNodes.Length; i++)
        {
            if (((AllNodes.allNodes[i].gridX == (currentNode.gridX + 2) || AllNodes.allNodes[i].gridX == (currentNode.gridX - 2)) && AllNodes.allNodes[i].gridZ == currentNode.gridZ) ||
                ((AllNodes.allNodes[i].gridZ == (currentNode.gridZ + 2) || AllNodes.allNodes[i].gridZ == (currentNode.gridZ - 2)) && AllNodes.allNodes[i].gridX == currentNode.gridX))
            {
                if (Mathf.RoundToInt(Mathf.Abs(AllNodes.allNodes[i].gridY - currentNode.gridY)) <= 1)
                    connectingNodes.Add(AllNodes.allNodes[i]);
            }
        }
    }
}