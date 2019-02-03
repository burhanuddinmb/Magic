using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBlock : MonoBehaviour
{    
    public Transform[] point;
    public float moveSpeed = 3;
    public int startPoint;
    public int targetPoint;
    public float time = 0; 
    void Start()
    {
        transform.position = point[startPoint].position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, point[targetPoint].position, moveSpeed * Time.deltaTime);

        if (transform.position == point[0].position)
        { 
            time += Time.deltaTime;
            if (Mathf.RoundToInt(time) > 2)
            {
                targetPoint = 1;
                time = 0.0f;
            }
        }
        if (transform.position == point[1].position)
        {            
            time += Time.deltaTime;
            if (Mathf.RoundToInt(time) > 2)
            {
                targetPoint = 0;
                time = 0.0f;
            }
        }
    }
}
