using UnityEngine;
using System.Collections;

public class toggleRotate : MonoBehaviour
{
    // numDegrees is the rotation per second
    public float numDegrees = 1;
    // spinFaster is how many degrees it'll rotate faster
    //public int spinFaster = 2;
    // speedMax is what's the max degrees it should rotate per second
    //public int speedMax = 3600;
    // whatAxis is the vector/axis we're going to rotate
    public Vector3 whatAxis = Vector3.forward;

    public float maxRotate = 15.0f;
    public float minRotate = 0.0f;
    public float curRotate = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(whatAxis * numDegrees); //NOTE: Not related to time anymore

        curRotate += numDegrees;
        curRotate = Mathf.Clamp(curRotate, minRotate, maxRotate);

        if (curRotate >= maxRotate) //if you hit the max rotation
        {
            numDegrees = numDegrees * -1 ; //change direction
        }
        else if (curRotate <= minRotate) //if you hit the min rotation
        {
            numDegrees = numDegrees * -1; //change direction 
        }
    }
}
