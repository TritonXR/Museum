using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingArrow : MonoBehaviour
{

    // numDegrees is the rotation per second
    public float numDegrees = 1;
    // spinFaster is how many degrees it'll rotate faster
    //public int spinFaster = 2;
    // speedMax is what's the max degrees it should rotate per second
    //public int speedMax = 3600;
    // whatAxis is the vector/axis we're going to rotate

    private Vector3 startMarker;
    private Vector3 endMarker;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start()
    {
        float yOffset = 1f;
        startMarker = new Vector3(transform.position.x, transform.position.y, transform.position.z+yOffset);
        endMarker = new Vector3(transform.position.x, transform.position.y, transform.position.z-yOffset);
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, Time.deltaTime * 270f);
    }

}