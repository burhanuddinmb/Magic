using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to the world at the start of the game, and run.
/// All Nodes and AccessibleNodes will be removed from the world, 
/// and then we can store it as a prefab. We can then take that prefab after stopping the game, 
/// and we can start re-adding them to the tiles that require them.
/// </summary>
public class RemoveAllNodesAndAccessibleNodes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AccessibleNodes[] acc = transform.GetChild(i).GetComponents<AccessibleNodes>();
            foreach (var node in acc)
            {
                Destroy(node);
            }
            Node[] nodes = transform.GetChild(i).GetComponents<Node>();
            foreach (var node in nodes)
            {
                Destroy(node);
            }
        }
    }
}
