using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour {

    public GameObject planeToResize;
    public GameObject myRibbon;

    public bool isExecuting;

	// Use this for initialization
	void Start () {
        isExecuting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Controller")
        {

            // if plane is big right now
            if (!planeToResize.GetComponent<Resizable>().isLittle)
            {
                if (!planeToResize.GetComponent<Resizable>().isImmune && !planeToResize.GetComponent<Resizable>().isGrabbed)
                {
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        planeToResize.GetComponent<Resizable>().ToggleImmune(true);
                        foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                        StartCoroutine(WaitAndResizeDown(planeToResize));
                    }
                }
            }

            // if the plane is already little
            else
            {
                if (!planeToResize.GetComponent<Resizable>().isImmune && !planeToResize.GetComponent<Resizable>().isGrabbed)
                {
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        planeToResize.GetComponent<Resizable>().ToggleImmune(true);
                        foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                        StartCoroutine(WaitAndResizeUp(planeToResize));
                    }
                }
                else
                {
                    Debug.Log("I am Plasma, the plane is Immune!!!!");
                }
            }


        }
    }

    IEnumerator WaitAndResizeDown(GameObject hittedObj)
    {
        GameObject tempRibbon = (GameObject)Instantiate(myRibbon, hittedObj.transform.position, hittedObj.transform.localRotation);

        // the safty check in case the ball is destroyed before it finish its duty
        hittedObj.GetComponent<Resizable>().DoSaftyCheck(tempRibbon);

        yield return new WaitForSeconds(1f);
        hittedObj.GetComponent<Resizable>().ResizeDown(null);
        yield return new WaitForSeconds(0.5f);
        Destroy(tempRibbon);

        yield return new WaitForSeconds(1f);

        hittedObj.GetComponent<Resizable>().ToggleImmune(false);
        isExecuting = false;


        yield return null;
    }


    IEnumerator WaitAndResizeUp(GameObject hittedObj)
    {
        GameObject tempRibbon = (GameObject)Instantiate(myRibbon, hittedObj.transform.position, hittedObj.transform.localRotation);

        // the safty check in case the ball is destroyed before it finish its duty
        hittedObj.GetComponent<Resizable>().DoSaftyCheck(tempRibbon);

        yield return new WaitForSeconds(1f);
        hittedObj.GetComponent<Resizable>().ResizeUp();
        yield return new WaitForSeconds(0.5f);
        Destroy(tempRibbon);

        yield return new WaitForSeconds(1f);

        hittedObj.GetComponent<Resizable>().ToggleImmune(false);

        isExecuting = false;

        yield return null;
    }



}
