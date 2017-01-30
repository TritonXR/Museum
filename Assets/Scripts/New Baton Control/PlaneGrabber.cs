using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGrabber : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Returns the plane at the player's gaze. 
    public GameObject getPlaneAtGaze() {
        Ray gaze = new Ray(this.transform.position, this.transform.forward);
        RaycastHit gazeHit;
        if(Physics.Raycast(gaze, out gazeHit, Mathf.Infinity)) {
            if(gazeHit.collider.GetComponent<IsPlane>()) {
                return gazeHit.collider.gameObject;
            }
            else {
                Debug.LogError("Object at gaze is not a plane. " + gazeHit.collider.gameObject.name + " " + gazeHit.point);
                return null;
            }
        }
        else {
            Debug.LogError("No object at gaze.");
            return null;
        }
    }
}
