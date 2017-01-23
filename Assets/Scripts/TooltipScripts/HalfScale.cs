using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(transform.localScale.x < 0.002)
            {
                
            }
            if (transform.localScale.x < 0.02)
            {
                transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            }
            else
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (transform.localScale.x < 0.02)
            {
                transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            }
            else
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

    }
}
