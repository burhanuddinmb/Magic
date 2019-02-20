using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveUseless : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AccessibleNodes[] accNodes = transform.GetChild(i).GetComponents<AccessibleNodes>();
            Node[] node = transform.GetChild(i).GetComponents<Node>();

            while (accNodes.Length > 1)
            {
                Destroy(transform.GetChild(i).GetComponent<AccessibleNodes>());
                accNodes = transform.GetChild(i).GetComponents<AccessibleNodes>();
            }
            while (node.Length > 1)
            {
                Destroy(transform.GetChild(i).GetComponent<Node>());
                node = transform.GetChild(i).GetComponents<Node>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
