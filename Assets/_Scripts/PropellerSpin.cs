using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour
{
    // numDegrees is the rotation per second (velocity)
    public float numDegrees = 0.0f;
    // spinFaster is how many degrees it'll rotate faster (acceleration)
    public float spinFaster = 0.0f;
    public float accelNum = 4.0f;
    // bounds the minimum and maximum speed to rotate
    public float speedMin = 0.0f;
    public float speedMax = 3600.0f;
    // whatAxis is the vector/axis we're going to rotate
    public Vector3 whatAxis = Vector3.forward;


  

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //When activated, change acceleration number
    public void activate()
    {
        if (spinFaster == 0)
        {
            spinFaster = accelNum;
        }
        else 
        {
            if (numDegrees >= 0)
            {
                spinFaster = -accelNum; 
            }
        }
    }

}
