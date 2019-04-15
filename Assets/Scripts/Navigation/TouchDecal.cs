using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDecal : MonoBehaviour
{
    [SerializeField] GameObject decal;
    GameObject decalRef;
    Transform world;

    //public static Vector3 positionToSlap;
    public static bool recreateDecal;
    public static Transform objectTransform;

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("World").transform;
        decalRef = Instantiate(decal, world);
        //decalRef.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (recreateDecal)
            ApplyDecal();
    }

    public static void AddTouchAnimation(Transform tr)
    {
        //Type1
        //Vector3 newPos = tr.localPosition;
        //
        //newPos.x += tr.localScale.x / 2;
        //newPos.y += tr.localScale.y + 0.01f;
        //newPos.z -= tr.localScale.z / 2;
        //positionToSlap = newPos;

        //Type2
        objectTransform = tr;

        recreateDecal = true;
       
    }

    void ApplyDecal()
    {
        //Type1
        //decalRef.transform.localPosition = positionToSlap;

        //Type2
        Vector3 newPos = Vector3.zero;
        newPos.x += objectTransform.localScale.x / 4;
        newPos.y += objectTransform.localScale.y/2 + 0.01f;
        newPos.z -= objectTransform.localScale.z / 4;
        decalRef.transform.parent = objectTransform;
        decalRef.transform.localPosition = newPos;

        //decalRef.SetActive(true);
        decalRef.GetComponent<ParticleSystem>().Stop();
        decalRef.GetComponent<ParticleSystem>().Play();
        recreateDecal = false;
    }
}
