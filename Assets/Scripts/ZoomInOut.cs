using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOut : MonoBehaviour
{
    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;

    [SerializeField]
    GameObject worldBase;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Input.touchCount:    " + Input.touchCount);
        if (Input.touchCount == 2)
        {
            
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;
            Debug.Log("touchesPrevPosDifference:    " + touchesPrevPosDifference);
            Debug.Log("touchesCurPosDifference:    " + touchesCurPosDifference);
            Debug.Log("--------------------------------------------------------------------------------------");
            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                 //mainCamera.orthographicSize += zoomModifier;
                worldBase.transform.localScale -= new Vector3(zoomModifier, zoomModifier, zoomModifier);
                if (worldBase.transform.localScale.x < 0.3f)
                {
                    worldBase.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
            }

            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                //mainCamera.orthographicSize -= zoomModifier;
                worldBase.transform.localScale += new Vector3(zoomModifier, zoomModifier, zoomModifier);
                if (worldBase.transform.localScale.x > 2.5f)
                {
                    worldBase.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }
            }

        }

        //mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 2f, 10f);
        //text.text = "Camera size " + mainCamera.orthographicSize;

    }
}
