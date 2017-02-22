using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputIndicator : MonoBehaviour {

    public Material unactiveMat;
    public Material activeMat;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activate() {
        GetComponent<Renderer>().material = activeMat;
    }
    public void deactivate() {
        GetComponent<Renderer>().material = unactiveMat;
    }

}
