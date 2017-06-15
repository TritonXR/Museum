using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPlane : MonoBehaviour {
    private void Awake()
    {
      gameObject.GetComponent<GroundedPlane>().enabled = false;
    }

    private void OnEnable()
    {
        Debug.Log("Script Enabled!");
    }

    private void OnTriggerEnter(Collider col)
    {

            if (col.name == "Plane (1)")
        {
            Debug.Log("Plane collided with " + col.name);
            //  gameObject.GetComponentInParent<Rigidbody>().drag = 5;
            gameObject.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;

            gameObject.GetComponentInParent<PlanePhysics>().enabled = false;
            //gameObject.GetComponentInParent<Rigidbody>().transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            gameObject.SetActive(false);
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
    }
}
