using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizable : MonoBehaviour {

    [HideInInspector]
    public bool isLittle;
    [HideInInspector]
    public bool isImmune;
    [HideInInspector]
    public bool isGrabbed;

    public float scaleMutiplier;

    private Vector3 storedSize;
    private Vector3 storedPos;

    private Quaternion storedRotation;

    

	// Use this for initialization
	void Start () {

        isLittle = false;

        isImmune = false;

        isGrabbed = false;

        storedSize = transform.localScale;

        GetComponent<ViveGrip_Grabbable>().enabled = false;

        StartCoroutine(WaitAndKinematic());

		
	}
    
    public void ResizeDown()
    {
        isLittle = true;
        GetComponent<ViveGrip_Grabbable>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        float currScaleValue = transform.localScale.x;
        float newScaleValue = currScaleValue * scaleMutiplier;
        transform.localScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);
        
    }


    public void ResizeUp()
    {
        transform.localScale = storedSize;
        transform.localPosition = storedPos;
        transform.localRotation = storedRotation;
        isLittle = false;
        GetComponent<ViveGrip_Grabbable>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

    }


    IEnumerator WaitAndKinematic()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Rigidbody>().isKinematic = true;
        storedRotation = transform.localRotation;
        storedPos = transform.localPosition;
        yield return null;
    }


    public void ToggleImmune(bool wantImmune)
    {
        if (wantImmune)
        {
            //Debug.Log("I am Plane, I am Immune!!!!");
            isImmune = true;
        }
        else
        {
            isImmune = false;
            //Debug.Log("I am Plane, and I am not Immune now!!!!");
        }
    }

    public void ToggleGrabbed(bool isGrabbing)
    {

        if (isGrabbing)
        {
            isGrabbed = true;
            Debug.Log("I am Grabbed");
        }

        else
        {
            isGrabbed = false;
            Debug.Log("I am no longer Grabbed");
        }

    }

    public void DoSaftyCheck(GameObject rainbow) {
        StartCoroutine(SaftyCheck(rainbow));
    }

    // check if the rain has been successfully destroyed after certain time
    IEnumerator SaftyCheck(GameObject rainbow)
    {

        yield return new WaitForSeconds(2.6f);

        if (isImmune)
        {
            isImmune = false;
        }
        
        if(rainbow != null)
        {
            Destroy(rainbow);
        }

    }
}
