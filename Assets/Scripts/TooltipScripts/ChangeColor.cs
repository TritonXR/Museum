using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Color temp= Color.white;
        temp.a = 0.1f;
        GetComponent<Renderer>().material.SetColor("_Color",temp);
        Debug.Log(GetComponent<Renderer>().material.color.a);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
