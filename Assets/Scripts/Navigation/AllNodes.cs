using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllNodes : MonoBehaviour
{
    public static Node[] allNodes;
    // Start is called before the first frame update
    void Start()
    {
        allNodes = GetComponentsInChildren<Node>();
    }

    public static void PathFind(Node startNode, Node targetNode, List<Node> path)
    {
        while (targetNode != startNode)
        {
            path.Insert(0, targetNode);
            targetNode = targetNode.parentNode;
        }
    }

    private static float Euclidean(Node start, Node end)
    {
        var x = start.gridX - end.gridX;
        var y = start.gridY - end.gridY;
        var z = start.gridZ - end.gridZ;

        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    private bool canClimb(Node start, Node end)
    {
        if (start.gridY == end.gridY)
        {
            return true;
        }
        else
        {
            foreach (var item in allNodes)
            {
                if (item.gridY <= start.gridY)
                { }
            }
        }

        return true;
    }

    public static void Sort(List<Node> node)
    {
        for (int i = 0; i < node.Count - 1; i++)
        {
            for (int j = 0; j < node.Count - i - 1; j++)
            {
                if (node[j].fcost > node[j+1].fcost)
                {
                    Node temp = node[j];
                    node[j] = node[j+1];
                    node[j+1] = temp;             
                }
            }
        }
    }

    public static List<Node> AStar(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        bool wasSuccessful = false;
        openSet.Add(startNode);
        //TODO sort openSet
        while (openSet.Count > 0)
        {
            Sort(openSet);
            Node currentNode = openSet[0];
            openSet.RemoveAt(0);

            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                wasSuccessful = true;
                break;
            }

            foreach (Node neighbor in currentNode.transform.GetComponent<AccessibleNodes>().connectingNodes)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newMoveCostToNeighbor = currentNode.gCost + Euclidean(currentNode, neighbor);

                if (newMoveCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMoveCostToNeighbor;
                    neighbor.hCost = Euclidean(neighbor, targetNode);
                    neighbor.parentNode = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        //TODO Sort
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        if (wasSuccessful)
        {
            PathFind(startNode, targetNode, path);
        }

        NullifyAllParent();
        return path;
    }

    private static void NullifyAllParent()
    {
        foreach (var node in allNodes)
        {
            node.parentNode = null;
            node.gCost = 0.0f;
            node.hCost = 0.0f;
        }
    }
}
