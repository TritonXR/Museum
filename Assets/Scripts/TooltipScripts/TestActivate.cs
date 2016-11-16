using UnityEngine;
using System.Collections;

public class TestActivate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //If T is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Call the activate function
            GetComponent<Rotate>().activate();
        }

    }
}
