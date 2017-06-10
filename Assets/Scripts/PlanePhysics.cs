using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour {
    public GameObject gObj;
    Rigidbody rb;
	// Use this for initialization
	void Awake () {
        rb = gObj.GetComponent<Rigidbody>();
        rb.mass = 0.5f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Speeds up slow moving throws
        if (System.Math.Pow(rb.velocity.x, 2) + System.Math.Pow(rb.velocity.z, 2) < 1)
        {
            rb.AddForce(rb.transform.forward * 3);
        }
        rb.AddForce(rb.transform.up * 3);

        rb.freezeRotation = true;
        rb.freezeRotation = false;
        if (rb.velocity != Vector3.zero)
        {
            // rb.freezeRotation = false;
            rb.transform.rotation = Quaternion.LookRotation(rb.velocity);
            // rb.freezeRotation = true;
        } else
        {
            rb.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            gObj.GetComponent<PlanePhysics>().enabled = false;
        }

    }
}
