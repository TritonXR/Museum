using UnityEngine;
using System.Collections;

public class toggleRotate : MonoBehaviour
{
    // numDegrees is the rotation per second
    public int numDegrees = 1;
    // spinFaster is how many degrees it'll rotate faster
    //public int spinFaster = 2;
    // speedMax is what's the max degrees it should rotate per second
    //public int speedMax = 3600;
    // whatAxis is the vector/axis we're going to rotate
    public Vector3 whatAxis = Vector3.forward;

    public int maxRotate = 15;
    public int minRotate = 0;
    public int curRotate = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(whatAxis * numDegrees * Time.deltaTime);

        curRotate += numDegrees; 

        if (curRotate >= maxRotate)
        {
            curRotate = maxRotate;
            numDegrees = numDegrees * -1; //toggle direction
        }
        else if (curRotate <= minRotate)
        {
            curRotate = minRotate;
            numDegrees = numDegrees * -1;
        }
    }
}
