using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    public GameObject Right;
    public GameObject Left;

    public GameObject targetPosition;

    

    void Update()
    {

        WorldRotate();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    if (swipeDistVertical > minSwipeDistY)
                    {
                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                        if (swipeValue > 0)
                            Rotateleft();

                        else if (swipeValue < 0)
                            RotateRight();
                    }

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)
                    {
                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        //if (swipeValue > 0)//right swipe
                        //     //MoveRight ();

                        //else if (swipeValue < 0)//left swipe
                        //     //MoveLeft ();
                    }
                    break;
                }
            }
        }

        void Rotateleft()
        {
            Debug.Log("Rotate left");
            Right.SetActive(true);
        }

        void RotateRight()
        {
            Debug.Log("Rotate right");
            Left.SetActive(true);
        }


        void WorldRotate()
        {
            if(Input.GetMouseButton(0))
            {
            //var targetPoint = targetPosition.transform.position;
            //var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);

            //Debug.Log("targetRotation:  "+ targetRotation);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);


                
            }    
            
           
        }
    }
