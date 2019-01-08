using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] point;
    public Transform camera;
    public float moveSpeed = 3;
    public int startPoint;
    public int targetPoint;
    private bool isReached;
    float yRotation = -90.0f;
    bool isRotating;
    Quaternion rotationGoal;
    Quaternion currentAngle;
    float timer;

    void Start()
    {
        currentAngle = transform.rotation;
        rotationGoal = Quaternion.identity;
        isRotating = false;
        isReached = false;
        transform.position = point[startPoint].position;
        timer = 0.0f;
    }

    void Update()
    {
        if (!isReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, point[targetPoint].position, moveSpeed * Time.deltaTime);

            if (transform.position == point[targetPoint].position)
            {
                targetPoint++;
                currentAngle = transform.rotation;
                rotationGoal = point[targetPoint - 1].rotation;
                isRotating = true;
                timer = 0.0f;
            }
        }
        if (targetPoint == 8)
        {
            camera.parent = null;
        }
        if (targetPoint == point.Length)
        {
            isReached = true;
            gameObject.SetActive(false);
        }
        if (isRotating)
        {
            timer += Time.deltaTime * 4.0f;
            transform.rotation = Quaternion.Slerp(currentAngle, rotationGoal, timer);
            isRotating = timer < 1.0f;
        }
    }
}
