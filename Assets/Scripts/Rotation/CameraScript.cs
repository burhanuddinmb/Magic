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

    public Vector3 rotationAngles;

    //This should be handled by other scripts
    public bool isAnythingImportantGoingOn;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        rotationAngles = world.transform.eulerAngles;
        isAnythingImportantGoingOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (!isAnythingImportantGoingOn)
            {
                CheckRotate();
            }
        }
        else if (Input.touchCount == 2)
        {
            CheckPanAndZoom();
        }
        world.transform.eulerAngles = rotationAngles;
    }

    void CheckRotate()
    {
        Touch touch = Input.touches[0];
        if (touch.phase == TouchPhase.Moved)
        {
            if (Mathf.Abs(touch.deltaPosition.x) < 2.0f)
                return;
            rotationAngles.y -= touch.deltaPosition.y * rotationSpeed * Time.deltaTime * Mathf.PI * 2;
        }
    }

    void CheckPanAndZoom()
    {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //Pan in "X"
        if (Mathf.Sign(touchZero.deltaPosition.x) == Mathf.Sign(touchOne.deltaPosition.x) && Mathf.Abs(touchZero.deltaPosition.x) > 4.0f && Mathf.Abs(touchOne.deltaPosition.x) > 4.0f)
        {
            Vector3 futurePosition = transform.position;
            futurePosition.x -= (touchZero.deltaPosition.x + touchOne.deltaPosition.x) * (Time.deltaTime * movementSpeed);
            transform.position = futurePosition;
        }
        //Pan in "Y"
        else if (Mathf.Sign(touchZero.deltaPosition.y) == Mathf.Sign(touchOne.deltaPosition.y) && Mathf.Abs(touchZero.deltaPosition.y) > 4.0f && Mathf.Abs(touchOne.deltaPosition.y) > 4.0f)
        {
            Vector3 futurePosition = transform.position;
            futurePosition.y -= (touchZero.deltaPosition.y + touchOne.deltaPosition.y) * (Time.deltaTime * movementSpeed);
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

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minCameraSize, maxCameraSize);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 70.0f, 100.0f);
            }
        }
    }
}
