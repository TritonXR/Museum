using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCheck : MonoBehaviour {

    public bool active;

	// Use this for initialization
	void Start () {
        active = false;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DestinationCheck>())
        {
            active = true;
            DestinationCheck.instance.checkPlane();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DestinationCheck>())
        {
            active = false;
        }
    }
}
