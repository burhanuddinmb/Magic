using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
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

    static int Partition(List<Node> nodes, int low,
                               int high)
    {
        Node pivot = nodes[high];

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than or equal to pivot 
            if (nodes[j].fcost <= pivot.fcost)
            {
                i++;

                // swap arr[i] and arr[j] 
                Node temp = nodes[i];
                nodes[i] = nodes[j];
                nodes[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        Node temp1 = nodes[i + 1];
        nodes[i + 1] = nodes[high];
        nodes[high] = temp1;

        return i + 1;
    }

    static void QuickSort(List<Node> nodes, int low, int high)
    {
        if (low < high)
        {
            /* pi is partitioning index, arr[pi] is  
            now at right place */
            int pi = Partition(nodes, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            QuickSort(nodes, low, pi - 1);
            QuickSort(nodes, pi + 1, high);
        }
    }

    public static List<Node> AStar(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        bool wasSuccessful = false;
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            QuickSort(openSet, 0, openSet.Count - 1);
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
