using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurned : MonoBehaviour {

    public GameObject sign;
	// Use this for initialization
	void Start () {
        sign.SetActive(true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if((transform.rotation.y>=.25 && transform.rotation.y<=.95) ) {
            Debug.Log("Player has turned around!");
            sign.SetActive(false);
        }
	}
}
