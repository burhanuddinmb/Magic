using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    // Touch distance to the object.
    private float dist;
    // Checking condition for touching the object.
    private bool dragging = false;
    // Distance of the object and the touch.
    private Vector3 offset;
    // Drag the object.
    private Transform toDrag;

    void Update()
    {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        // Started the touch.
        if (touch.phase == TouchPhase.Began)
        {
            // Finger starts the touch.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            // Finger on object.
            if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Draggable"))
            {
                Debug.Log("Here");
                // Object to finger.
                toDrag = hit.transform;
                dist = hit.transform.position.z - Camera.main.transform.position.z;
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                offset = toDrag.position - v3;
                dragging = true;
            }
        }
        // Check finger drag the object to the display.
        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            // Drag object at particular distance.
            toDrag.position = v3 + offset;
        }
        // Finger release the Object/Screen.
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
        }
    }
}
