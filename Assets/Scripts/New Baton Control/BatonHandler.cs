using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonHandler : MonoBehaviour {
    
    //Combination Panels 
    public BatonControl ULPanel;
    public BatonControl URPanel;
    public BatonControl BLPanel;
    public BatonControl BRPanel;
    public BatonControl LPanel;
    public BatonControl RPanel;

    public static BatonHandler instance;
    public PlaneGrabber planeGrabber;
    public GameObject plane;


    public float movementMult = 3.0f;
    public float dur = 0.5f;

    bool moving;
    bool rotating;

    Coroutine rotCoroutine;
    Coroutine moveCoroutine;

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
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void checkCombinations()
    {
        //Forward
        plane = planeGrabber.getPlaneAtGaze();

        if (ULPanel.active && URPanel.active)
        {
            if (moving)
            {
                moving = false;
                StopCoroutine(moveCoroutine);
            }
            if (plane != null) {
                moveCoroutine = StartCoroutine(planeForward(plane));
     
            }    
        }

        //Backward
        else if (BLPanel.active && BRPanel.active)
        {
            if (plane != null)
            {
                if (moving)
                {
                    moving = false;
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(planeBackward(plane));
            }
        }

        //Rotate Left
        else if (ULPanel.active && BRPanel.active)
        {
            if (plane != null)
            {
                if (rotating)
                {
                    rotating = false;
                    StopCoroutine(rotCoroutine);
                }
                rotCoroutine = StartCoroutine(planeLeft(plane));
            }
        }

        //Rotate Right
        else if (BLPanel.active && URPanel.active)
        {
            if (plane != null)
            {
                if (rotating)
                {
                    rotating = false;
                    StopCoroutine(rotCoroutine);
                }
                rotCoroutine = StartCoroutine(planeRight(plane));
            }
        }

        //Stop
        else if (LPanel.active && RPanel.active) {
            if(plane!=null) {
                StopAllCoroutines();
            }
        }


    }

    IEnumerator planeForward(GameObject plane) {
        moving = true;
        Debug.Log("Start Forward");
        Vector3 startPos = plane.transform.position;
        Vector3 endPos = plane.transform.position + plane.transform.forward * movementMult * 2;

        for (float j = 0; j < dur; j += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(startPos, endPos, j / dur);
            plane.transform.position = newPos;
            yield return null;
        }
        moving = false;
    }

    IEnumerator planeBackward(GameObject plane)
    {
        moving = true;
        Debug.Log("Start Backward");
        Vector3 startPos = plane.transform.position;
        Vector3 endPos = plane.transform.position - (plane.transform.forward * movementMult);

        for (float j = 0; j < dur; j += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(startPos, endPos, j / dur);
            plane.transform.position = newPos;
            yield return null;
        }
        moving = false;
    }

    IEnumerator planeRight(GameObject plane)
    {
        rotating = true;
        Debug.Log("Start Right");
        Quaternion startRot = plane.transform.rotation;
        Vector3 endRotEuler = startRot.eulerAngles;
        endRotEuler.y += 20.0f;
        Quaternion endRot = Quaternion.Euler(endRotEuler);


        for (float i = 0; i < dur; i += Time.deltaTime)
        {
            Quaternion newRot = Quaternion.Lerp(startRot, endRot, i / dur);
            plane.transform.rotation = newRot;
            yield return null;
        }
        rotating = false;
    }

    IEnumerator planeLeft(GameObject plane)
    {
        rotating = true;
        Debug.Log("Start Left");
        Quaternion startRot = plane.transform.rotation;
        Vector3 endRotEuler = startRot.eulerAngles;
        endRotEuler.y -= 20.0f;
        Quaternion endRot = Quaternion.Euler(endRotEuler);


        for (float i = 0; i < dur; i += Time.deltaTime)
        {
            Quaternion newRot = Quaternion.Lerp(startRot, endRot, i / dur);
            plane.transform.rotation = newRot;
            yield return null;
        }
        rotating = false;
    }

}
