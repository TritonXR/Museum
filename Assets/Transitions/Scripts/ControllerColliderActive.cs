using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerColliderActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        if(col.name == "[CameraRig]")
        {
            col.GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        col.GetComponentInChildren<CapsuleCollider>().enabled = false;
    }
}
