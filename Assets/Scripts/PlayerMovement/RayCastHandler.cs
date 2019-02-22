using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHandler : MonoBehaviour
{

    GameObject selectedObject;
    [SerializeField] Transform world;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    selectedObject = hit.transform.parent.gameObject;
                    if (selectedObject && selectedObject.tag != "InteractableObjects")
                    {
                        selectedObject = null;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (selectedObject)
                {
                    selectedObject.transform.rotation = Quaternion.Euler(0, touch.deltaPosition.x * rotationSpeed, 0);
                }
                else
                {
                    world.rotation = Quaternion.Euler(0, touch.deltaPosition.x * rotationSpeed, 0);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                selectedObject = null;
            }
        }
    }
}
