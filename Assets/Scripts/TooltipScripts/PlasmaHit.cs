using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaHit : MonoBehaviour {


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

        //Debug.Log("I am Plasma, I hitted the plane");

        if(collision.gameObject.tag == "resizable")
        {
            GameObject hittedObj = collision.gameObject;

            // if plane is big right now
            if (!hittedObj.GetComponent<Resizable>().isLittle)
            {
                if (!hittedObj.GetComponent<Resizable>().isImmune)
                {
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        hittedObj.GetComponent<Resizable>().ToggleImmune(true);
                        foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                        StartCoroutine(WaitAndResizeDown(hittedObj));
                    }
                }
            }

            // if the plane is already little
            else
            {
                if (!hittedObj.GetComponent<Resizable>().isImmune)
                {
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        hittedObj.GetComponent<Resizable>().ToggleImmune(true);
                        foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                        StartCoroutine(WaitAndResizeUp(hittedObj));
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
        yield return new WaitForSeconds(1f);
        hittedObj.GetComponent<Resizable>().ResizeDown();
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
