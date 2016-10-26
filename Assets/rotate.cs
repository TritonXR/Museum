using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
    // numDegrees is the rotation per second
    public int numDegrees = 1;
    // spinFaster is how many degrees it'll rotate faster
    public int spinFaster = 2;
    // speedMax is what's the max degrees it should rotate per second
    public int speedMax = 3600;
    // whatAxis is the vector/axis we're going to rotate
    public Vector3 whatAxis = Vector3.forward;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(whatAxis * numDegrees * Time.deltaTime);

        while (numDegrees < speedMax){
            numDegrees += spinFaster;
            break;
        }
    }
}
