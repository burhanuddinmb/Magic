using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandledByButton : MonoBehaviour
{
    Node node;
    AccessibleNodes accessibleNodes;

    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;

    void Start()
    {
        node = GetComponent<Node>();
        accessibleNodes = GetComponent<AccessibleNodes>();
    }


    private void Update()
    {
        if (x)
            CheckForAccessibleNodesX();
        if (y)
            CheckForAccessibleNodesY();
        if (z)
            CheckForAccessibleNodesZ();
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

    void CheckForAccessibleNodesX()
    {
        if (Mathf.Abs(transform.localPosition.x - node.gridX) >= 1.0f)
        {
            node.gridX = (int)transform.localPosition.x;
            ReAdjustNodes();
        }
    }

    void CheckForAccessibleNodesY()
    {
        if (Mathf.Abs(transform.localPosition.y - node.gridY) >= 1.0f)
        {
            node.gridY = (int)transform.localPosition.y;
            ReAdjustNodes();
        }
    }

    void CheckForAccessibleNodesZ()
    {
        if (Mathf.Abs(transform.localPosition.z - node.gridZ) >= 1.0f)
        {
            node.gridZ = (int)transform.localPosition.z;
            ReAdjustNodes();
        }
    }
}
