using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{

    public float minY;
    public float maxY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        //float distance_to_screen = 2.0f;//Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        /* Debug.Log("distance_to_screen:  "   + distance_to_screen);
         Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(minY, Input.mousePosition.y, distance_to_screen));


         transform.position = new Vector3(minY, pos_move.y, maxY);

         if (minY > -2.93)
             pos_move.y = -2.93f;

             if (maxY > 1.1)
                 pos_move.y = 1.1f;*/
        transform.position = new Vector3(minY, Input.mousePosition.y, maxY);
        Debug.Log("OnMouseDrag");
    }
}
