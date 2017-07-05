using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour {
    public GameObject physicsCol;
    Rigidbody rb;
	// Use this for initialization
	void OnEnable () {
        Debug.Log("PlanePhysics enabled!");
        rb = gameObject.GetComponent<Rigidbody>();
       // physicsCol.SetActive(true);
       // physicsCol.GetComponent<GroundedPlane>().enabled = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Speeds up slow moving throws
        double fVector = (System.Math.Pow(rb.velocity.x, 2) + System.Math.Pow(rb.velocity.z, 2));
       /* if ( fVector < 10 && fVector > 5)
        {
            rb.AddForce(rb.transform.forward * 6);
        }*/
        Debug.Log("Adding Upward Force!");
        rb.AddForce(rb.transform.up * 6);

        if (rb.velocity != Vector3.zero)
        {
          
            rb.transform.rotation = Quaternion.LookRotation(rb.velocity);
           
        }

    }
    private void OnTriggerEnter(Collider col)
    {

        if (col.name == "Plane (1)")
        {
            Debug.Log("Plane collided with " + col.name);
           rb.velocity = Vector3.zero;
           gameObject.GetComponent<PlanePhysics>().enabled = false;
          
        }
    }

}
