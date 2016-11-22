using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
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
    public bool isActive = false;

    /* Idea: Change spinFaster based on percentage of max so that the amount to
     * increase or decrease isnt't linear (e.g. always going down 1)
     */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(whatAxis * numDegrees * Time.deltaTime ); //rotates 
       
        if (numDegrees <= speedMax && numDegrees >= speedMin){
            numDegrees +=  spinFaster; //* Time.deltaTime; //changes velocity
            numDegrees = Mathf.Clamp(numDegrees, speedMin, speedMax);
        }


        if (numDegrees <= 0)
        {
            spinFaster = 0; //Set velocity to 0
        }
    }

    //When activated, change acceleration number
    public void activate(){
        if (!isActive) //change acceleration
        {
            spinFaster = accelNum;
            isActive = true;
        }
        else //reset the acceleration
        {
            Deactivate();
        }
    }

    //Flips acceleration direction; should only be called by activate to slow down rotation
    public void Deactivate()
    {
        spinFaster = -accelNum; //change acceleration to be negative (slows velocity down)
        isActive = false;
    }

}
