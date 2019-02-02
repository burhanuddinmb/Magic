using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDraggableBlock : MonoBehaviour
{
    //public float distancefromCamera;
    Rigidbody r;
    private bool drag;
    Vector3 pos;

    // Use this for initialization
    void Start()
    {
        //distancefromCamera = Vector3.Distance (transform.position, cam.transform.position);
        r = transform.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (drag == true)
        {
            DragObj();
        }
    }
    void DragObj()
    {
        if (Input.GetMouseButton(0))
        {
            pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            r.velocity = (pos - transform.position) * 5;
        }
    }
    void OnMouseDown()
    {
        drag = true;
    }
    void OnMouseUp()
    {
        if (drag == true)
        {
            drag = false;
        }
    }
}
