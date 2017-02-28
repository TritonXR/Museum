using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour {


    public bool isLittle;
	// Use this for initialization
	void Start () {

        isLittle = false;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ResizeDown()
    {
        float currScaleValue = transform.localScale.x;
        float newScaleValue = currScaleValue * 0.25f;
        transform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);
        isLittle = true;
    }
}
