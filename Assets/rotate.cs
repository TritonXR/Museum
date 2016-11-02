using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
    // numDegrees is the rotation per second
    public float numDegrees = 1.0f;
    // spinFaster is how many degrees it'll rotate faster
    public float spinFaster = 2.0f;
    // bounds the minimum and maximum speed to rotate
    public float speedMin = 0.0f;
    public float speedMax = 3600.0f;
    // whatAxis is the vector/axis we're going to rotate
    public Vector3 whatAxis = Vector3.forward;


    /* Idea: Change spinFaster based on percentage of max so that the amount to
     * increase or decrease isnt't linear (e.g. always going down 1)
     */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(whatAxis * numDegrees * Time.deltaTime); //rotates 
       
        if (numDegrees <= speedMax && numDegrees >= speedMin){
            numDegrees += spinFaster;
            numDegrees = Mathf.Clamp(numDegrees, speedMin, speedMax);
        }
    }
}
