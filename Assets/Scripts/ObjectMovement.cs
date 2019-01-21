using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{

    GameObject selectedObject;
    float movementSpeed;

    void Start()
    {
        movementSpeed = 0.05f;
    }

    void Update()
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
                    if (hit.transform.tag == "VerticalMovers")
                    {
                        selectedObject = hit.transform.parent.gameObject;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (selectedObject)
                {
                    selectedObject.transform.position += new Vector3(0, touch.deltaPosition.y * movementSpeed, 0);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                selectedObject = null;
            }
        }
    }
}