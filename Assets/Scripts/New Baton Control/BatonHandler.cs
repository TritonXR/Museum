using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonHandler : MonoBehaviour {
    
    //Combination Panels 
    public BatonControl LPanel;
    public BatonControl RPanel;


    public static BatonHandler instance;
    public PlaneGrabber planeGrabber;
    public GameObject plane;


    public float movementMult = 5.0f;
    public float dur = 1.0f;
	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void checkCombinations()
    {
        if(LPanel.active && RPanel.active)
        {
            plane = planeGrabber.getPlaneAtGaze();
            if (plane != null) {
                StartCoroutine(planeForward(plane));
            }    
        }
    }

    IEnumerator planeForward(GameObject plane) {
        Debug.Log("Start Forward");
        Vector3 startPos = plane.transform.position;
        Vector3 endPos = plane.transform.position + plane.transform.forward * movementMult;

        for (float j = 0; j < dur; j += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(startPos, endPos, j / dur);
            plane.transform.position = newPos;
            Debug.Log("Loop");
            yield return null;
        }

    }
     
}
