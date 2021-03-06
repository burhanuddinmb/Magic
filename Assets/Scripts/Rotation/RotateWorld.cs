﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {
        isTouchActive = false;
        isObjectSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchInput = Input.GetTouch(0);
           // Debug.Log("touchInput:      "+ touchInput);
            if (Input.touchCount == 1)
            {
                //Debug.Log("Touch to rotate world");
                switch (touchInput.phase)
                {
                    case TouchPhase.Began:
                        Ray ray = Camera.main.ScreenPointToRay(touchInput.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 100.0f))
                        {
                            if (hit.transform.tag == "Nodes")
                            {
                                isObjectSelected = true;
                            }
                        }
                        startTime = Time.time;
                        initialTouchSpace = touchInput.position;
                        break;

                    case TouchPhase.Moved:
                        deltaTouchSpace = initialTouchSpace - touchInput.position;
                        initialTouchSpace = touchInput.position;
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
                   // player.GetComponent<PlayerController>().StopPlayer();
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

            //Debug.Log("transform.rotation:  " + transform.rotation);
        }
        //CalculateCenter();
       
        
    }

    /*void CalculateCenter()
    {
         

    }*/
   /* public class Player
    {
        public float x { get; set; } 
        public float z { get; set; }
    }
    public void CalculateCenter()
    {
        List<Player> playersInGame = new List<Player>();
        // 
        playersInGame.Add(new Player { x = gameObject.GetComponentInChildren<Node>().GetGridX(), z = gameObject.GetComponentInChildren<Node>().GetGridZ()});
        //Debug.Log("GridX :  " + gameObject.GetComponentInChildren<Node>().GetGridX());
        //Debug.Log("GridZ :  " + gameObject.GetComponentInChildren<Node>().GetGridZ());
        var totalX = 0f; 
        var totalZ = 0f;
        playersInGame.Add(new Player { x = 0, z = 0 });
        playersInGame.Add(new Player { x = 10, z = 0 });
        foreach (var player in playersInGame)
        {
            totalX += player.x;
            totalZ += player.z;
        }
        var centerX = totalX / playersInGame.Count; 
        var centerZ = totalZ / playersInGame.Count;

        Debug.Log("centerX : " + centerX); 
        Debug.Log("centerZ : " + centerZ);
    }*/
}
