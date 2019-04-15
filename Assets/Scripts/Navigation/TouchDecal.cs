using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDecal : MonoBehaviour
{
    [SerializeField] GameObject decal;
    GameObject decalRef;
    Transform world;

    public static Vector3 positionToSlap;
    public static bool recreateDecal;

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

    void SlapDecal()
    {
        decalRef.transform.position = positionToSlap;
    }

    public static void AddTouchAnimation(Transform tr)
    {
        Vector3 newPos = tr.localPosition;
        
        newPos.x += tr.localScale.x / 2;
        newPos.y += tr.localScale.y + 0.01f;
        newPos.z -= tr.localScale.z / 2;
        
        positionToSlap = newPos;
        recreateDecal = true;
    }

    void ApplyDecal()
    {
        decalRef.transform.localPosition = positionToSlap;
        //decalRef.SetActive(true);
        decalRef.GetComponent<ParticleSystem>().Stop();
        decalRef.GetComponent<ParticleSystem>().Play();
        recreateDecal = false;
    }
}
