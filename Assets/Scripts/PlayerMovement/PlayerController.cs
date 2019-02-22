using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    float startTime;
    float endTime;
    Vector3 destination;
    bool touched;

    // Start is called before the first frame update
    void Start()
    {
        touched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    
                    startTime = Time.time;
                    destination = hit.point;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                touched = true;
                endTime = Time.time;
            }
        }

        if (touched)
        {
            if (startTime - endTime < 0.1f)
            {
                agent.SetDestination(destination);
                agent.isStopped = false;
            }
            touched = false;
        }
    }

    public void StopPlayer()
    {
        agent.isStopped = true;
    }
}
