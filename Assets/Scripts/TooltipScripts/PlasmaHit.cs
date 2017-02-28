using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I am Plasma, I hitted the plane");
    }
}
