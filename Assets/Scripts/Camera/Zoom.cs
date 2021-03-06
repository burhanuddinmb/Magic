﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Camera camera;
    //public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float maxZoom = 10f; 
    public float minZoom = 2f;
    //public GameObject rink;

    private void Start()
    {
        camera = GetComponent<Camera>();
        //Camera.main.orthographicSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
       
    }

    void Update()
    {
        
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            //Debug.Log("Touch to camera zoom");
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            //if (camera.orthographic)
            //{
            //    // ... change the orthographic size based on the change in distance between the touches.
            //    camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            //    // Make sure the orthographic size never drops below zero.
            //    camera.orthographicSize = Mathf.Max(22.0f, 5.1f);//camera.orthographicSize
            //    Debug.Log("camera.orthographicSize:     "+camera.orthographicSize);
            //}
            //else
            //{
            //    // Otherwise change the field of view based on the change in distance between the touches.
            //    camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            //    // Clamp the field of view to make sure it's between 0 and 180.
            //    camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 4.0f, 15.0f);
            //}

            if (camera.orthographic)
            {
                float i = camera.orthographicSize + deltaMagnitudeDiff * orthoZoomSpeed;

                if (i >= maxZoom)
                { 
                    camera.orthographicSize = maxZoom;
                }
                else if (i <= minZoom)
                { 
                    camera.orthographicSize = minZoom;
                }
                else
                { 
                    camera.orthographicSize = i;
                }
            }
        }
    }
}
