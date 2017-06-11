using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour {
    public GameObject physicsCol;
    Rigidbody rb;
	// Use this for initialization
	void Awake () {
        rb = gameObject.GetComponent<Rigidbody>();
        physicsCol.SetActive(true);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Speeds up slow moving throws
        if ((System.Math.Pow(rb.velocity.x, 2) + System.Math.Pow(rb.velocity.z, 2) < 1))
        {
            rb.AddForce(rb.transform.forward * 6);
        }
        rb.AddForce(rb.transform.up * 6);

        if (rb.velocity != Vector3.zero)
        {
          
            rb.transform.rotation = Quaternion.LookRotation(rb.velocity);
           
        }/* else
        {
            Debug.Log("Grounded.");
            grounded = true;
           // gameObject.GetComponent<Rigidbody>().drag = 5;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            gameObject.GetComponent<PlanePhysics>().enabled = false;
        }*/

    }
}
