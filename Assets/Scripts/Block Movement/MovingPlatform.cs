using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float thrust;
    Rigidbody rb;
    bool isTouchActive;
    bool isObjectSelected;
    [SerializeField] bool isVertical;
    float startTime;

    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;

    Vector3 futurePosition;
    [SerializeField] GameObject player;
    
    void Start()
    {
        thrust = 2.0f;
        rb = transform.GetChild(1).GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.parent == transform)
                    {
                        Debug.Log(hit.transform.name);
                        isObjectSelected = true;
                    }
                }
                startTime = Time.time;
                initialTouchSpace = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (isObjectSelected)
                {
                    deltaTouchSpace = initialTouchSpace - touch.position;
                    initialTouchSpace = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isObjectSelected = false;
            }

            if (!isTouchActive && isObjectSelected)
            {
                float timeChange = Time.time - startTime;

                if (timeChange > 0.1f)
                {
                    isTouchActive = true;
                    player.GetComponent<PlayerController>().StopPlayer();
                }
            }
        }

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;
            futurePosition = transform.position;
            futurePosition.y = transform.position.y - (deltaTouchSpace.y * Time.deltaTime * thrust);
            if (isVertical)
            {
                rb.MovePosition(futurePosition);

            }
        }
    }
}
