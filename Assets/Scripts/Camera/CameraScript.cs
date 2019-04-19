using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject world;

    [SerializeField] float rotationSpeed;

    [SerializeField] float zoomSpeed;
    [SerializeField] float movementSpeed;

    [SerializeField] float minCameraSize;
    [SerializeField] float maxCameraSize;

    float lerpSpeed = 0.2f;
    float movePos;
    float timer;
    Vector3 theSpeed;
    Vector3 avgSpeed;
    Vector3 targetSpeedX;
    
    //This should be handled by other scripts
    public bool isAnythingImportantGoingOn;
    bool isDragging;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
        camera = GetComponent<Camera>();
        camera.orthographic = true;
        isAnythingImportantGoingOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnythingImportantGoingOn)
        {
            CheckRotate();
        }

        if (Input.touchCount == 2)
        {
            CheckPanAndZoom();
        }
    }

    void CheckRotate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                theSpeed = Vector3.zero;
            }

            if (timer > 0.16f)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    isDragging = true;
                    movePos = -touch.deltaPosition.x;
                    movePos *= (1080.0f / Screen.width);
                    theSpeed = new Vector3(movePos, 0.0f, 0.0F);
                    avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime);
                }
                if (touch.phase == TouchPhase.Stationary)
                {
                    isDragging = false;
                    //theSpeed = avgSpeed;
                    float i = Time.deltaTime * lerpSpeed;
                    theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, 0.02f);
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        else
        {
            isDragging = false;
            float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, 0.05f);
            timer = 0.0f;
        }
        world.transform.Rotate(world.transform.up * theSpeed.x * rotationSpeed, Space.World);
    }

    void CheckPanAndZoom()
    {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


        if ((Mathf.Sign(touchZero.deltaPosition.x) == Mathf.Sign(touchOne.deltaPosition.x) && Mathf.Abs(touchZero.deltaPosition.x) > 4.0f && Mathf.Abs(touchOne.deltaPosition.x) > 4.0f) || //Pan in "X"
            (Mathf.Sign(touchZero.deltaPosition.y) == Mathf.Sign(touchOne.deltaPosition.y) && Mathf.Abs(touchZero.deltaPosition.y) > 4.0f && Mathf.Abs(touchOne.deltaPosition.y) > 4.0f)) //Pan in "Y"
        {
            Vector3 futurePosition = transform.position;
            futurePosition.x -= (touchZero.deltaPosition.x + touchOne.deltaPosition.x) * (Time.deltaTime * movementSpeed) * (1080.0f / Screen.width);
            futurePosition.y -= (touchZero.deltaPosition.y + touchOne.deltaPosition.y) * (Time.deltaTime * movementSpeed) * (1920.0f / Screen.height);
            transform.position = futurePosition;
        }
        else //Zoom
        {
            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (Mathf.Abs(deltaMagnitudeDiff) < 2.0f)
                return;

            // Change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * zoomSpeed * (1920 / Screen.height);

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minCameraSize, maxCameraSize);
        }
    }
}
