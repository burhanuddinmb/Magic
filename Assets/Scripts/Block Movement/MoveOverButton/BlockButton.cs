using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButton : MonoBehaviour
{
    // Start is called before the first frame update
    Node theBlockNode;

    bool blockNodeTracker;
    bool processing;
    bool waitForChange;
    bool setPos;

    [SerializeField] Texture texSet;
    [SerializeField] Texture texUnset;
    Texture objectTexture;

    [SerializeField] GameObject objectToMove;

    [SerializeField] Vector3 setPosition;
    [SerializeField] Vector3 unsetPosition;

    [SerializeField] float timeToMove;
    float timePassed;

    void Start()
    {
        //objectTexture = transform.GetComponent<Material>().mainTexture;
        theBlockNode = GetComponent<Node>();
        timePassed = 0.0f;
        waitForChange = false;
        setPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (theBlockNode.isOccupied && !waitForChange)
        {
            waitForChange = true;
            blockNodeTracker = true;
            setPos = !setPos;
        }
        if (waitForChange && !theBlockNode.isOccupied)
        {
            waitForChange = false;
        }

        if (blockNodeTracker)
        {
            blockNodeTracker = false;
            processing = true;
        }

        if (processing)
        {
            if (setPos)
                timePassed += Time.deltaTime;
            else
                timePassed -= Time.deltaTime;

            objectToMove.transform.localPosition =  Vector3.Lerp(unsetPosition, setPosition, timePassed / timeToMove);

            if (timePassed >= timeToMove)
            {
                objectToMove.transform.localPosition = setPosition;
                processing = false;
                timePassed = timeToMove;
            }

            if (timePassed <= 0)
            {
                objectToMove.transform.localPosition = unsetPosition;
                processing = false;
                timePassed = 0;
            }
        }
    }
}
