using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBlock : MonoBehaviour
{    
    public Transform[] point;
    public float moveSpeed = 3;
    public int startPoint;
    public int targetPoint;

    void Start()
    {
        transform.position = point[startPoint].position;
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, point[targetPoint].position, moveSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, point[targetPoint].position, moveSpeed * Time.deltaTime);
        if (transform.position == point[targetPoint].position)
        {
            targetPoint++;
            if (targetPoint == point.Length)
            {
                targetPoint = 0;
            }
        }
    }
}
