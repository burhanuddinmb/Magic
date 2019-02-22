using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public float rotateSpeed;
    public float rotateFriction;
    public float rotateSmoothness;

    private float rotateValue;
    Quaternion from;
    Quaternion to;

    public Transform target;
    public float speed = 5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            rotateValue += Input.GetAxis("Horizontal") * rotateSpeed * rotateFriction;
            from = transform.rotation;
            to = Quaternion.Euler(0, rotateValue, 0);

            transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * rotateSmoothness);

            Debug.Log("Rotation:    " + transform.rotation);
        //}

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed);
    }
}
