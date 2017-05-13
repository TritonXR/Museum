using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour {

    public bool active;
	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(active = true) {
            GetComponent<MeshRenderer>().enabled = true;
        }
	}
}
