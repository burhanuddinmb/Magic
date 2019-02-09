using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class device : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SystemInfo.graphicsDeviceName);
        Debug.Log(SystemInfo.graphicsDeviceID); 
        Debug.Log(SystemInfo.graphicsDeviceType); 
    }
}
