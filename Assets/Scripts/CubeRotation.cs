using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        isTouchActive = false;
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
                        initialTouchSpace = touchInput.position;
                        break;

                    case TouchPhase.Moved:
                        deltaTouchSpace = initialTouchSpace - touchInput.position;
                        initialTouchSpace = touchInput.position;
                        isTouchActive = true;
                        break;
                }
            }
        }

        if (isTouchActive)
        {
            isTouchActive = false;
            Resulting_Value_from_Input += deltaTouchSpace.x * Rotation_Speed * Rotation_Friction; //You can also use "Mouse X"
            Quaternion_Rotate_From = transform.rotation;
            Quaternion_Rotate_To = Quaternion.Euler(0, Resulting_Value_from_Input, 0);
            transform.rotation = Quaternion_Rotate_To;
            //transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
        }
                
        /*
        Resulting_Value_from_Input += Input.GetAxis("Horizontal") * Rotation_Speed * Rotation_Friction; //You can also use "Mouse X"
        Quaternion_Rotate_From = transform.rotation;
        Quaternion_Rotate_To = Quaternion.Euler(0, Resulting_Value_from_Input, 0);
       // transform.rotation = Quaternion_Rotate_To;
        transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
        */
    }
}
