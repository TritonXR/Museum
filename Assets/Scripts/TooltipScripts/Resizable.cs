using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour {


    public bool isLittle;

    public bool isImmune;

    //public bool isGrabbed;

    private Vector3 storedSize;

    private Quaternion storedRotation;

    

	// Use this for initialization
	void Start () {

        isLittle = false;

        isImmune = false;

        //isGrabbed = false;

        storedSize = transform.localScale;

        storedRotation = transform.localRotation;

        GetComponent<ViveGrip_Grabbable>().enabled = false;

        StartCoroutine(WaitAndKinematic());

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void ResizeDown()
    {
        isLittle = true;
        GetComponent<ViveGrip_Grabbable>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        float currScaleValue = transform.localScale.x;
        float newScaleValue = currScaleValue * 0.25f;
        transform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);
        
    }


    public void ResizeUp()
    {
        Debug.Log("I am plane, I am about to resize up!!!!!");
        
    }


    IEnumerator WaitAndKinematic()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Rigidbody>().isKinematic = true;
        yield return null;
    }


    public void ToggleImmune(bool wantImmune)
    {
        if (wantImmune)
        {
            Debug.Log("I am Plane, I am Immune!!!!");
            isImmune = true;
        }
        else
        {
            isImmune = false;
            Debug.Log("I am Plane, and I am not Immune now!!!!");
        }
    }

    //public void ToggleGrabbed(bool isGrabbing)
    //{

    //    if (isGrabbing)
    //    {
    //        isGrabbed = true;
    //        Debug.Log("I am Grabbed");
    //    }

    //    else
    //    {
    //        isGrabbed = false;
    //        Debug.Log("I am no longer Grabbed");
    //    }
        
    //}
}
