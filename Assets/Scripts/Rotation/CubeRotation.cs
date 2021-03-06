﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float Rotation_Speed;
    public float Rotation_Friction; 
    public float Rotation_Smoothness;

    private float Resulting_Value_from_Input;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;


    Vector2 initialTouchSpace;
    Vector2 deltaTouchSpace;
    bool isTouchActive;
    float startTime;
    bool isObjectSelected;

    public GameObject player;

    // Use this for initialization
    void Start()
    {
        isTouchActive = false;
        isObjectSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch touchInput = Input.GetTouch(0);

            if (Input.touchCount == 1)
            {
                switch (touchInput.phase)
                {
                    case TouchPhase.Began:
                        Ray ray = Camera.main.ScreenPointToRay(touchInput.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100.0f))
                        {
                            if (hit.transform.parent == this.transform)
                            {
                                isObjectSelected = true;
                            }
                        }
                        startTime = Time.time;
                        initialTouchSpace = touchInput.position;
                        break;

                    case TouchPhase.Moved:
                        if (isObjectSelected)
                        {
                            deltaTouchSpace = initialTouchSpace - touchInput.position;
                            initialTouchSpace = touchInput.position;
                        }
                        break;

                    case TouchPhase.Ended:
                        isObjectSelected = false;
                        break;
                }
            }
            if (!isTouchActive && isObjectSelected)
            {
                float timeChange = Time.time - startTime;

                if (timeChange > 0.1f)
                {
                    isTouchActive = true;
                }
            }
        }

        if (isTouchActive && isObjectSelected)
        {
            isTouchActive = false;
            Resulting_Value_from_Input += deltaTouchSpace.x * Rotation_Speed * Rotation_Friction; 
            Quaternion_Rotate_From = transform.rotation;
            Quaternion_Rotate_To = Quaternion.Euler(0, Resulting_Value_from_Input, 0);
            transform.rotation = Quaternion_Rotate_To;
        }
    }
}
