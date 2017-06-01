using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked2 : MonoBehaviour {

    public GameObject planeToResize;
    public GameObject myRibbon;
    public Transform spawnLoc;


    public Text text;
    public bool isExecuting;


    private Color tempColor;
	// Use this for initialization
	void Start () {
        isExecuting = false;
        tempColor = text.color;
    }
	
    void OnTriggerEnter(Collider collision)
    {

        text.color = Color.green;
        Debug.Log("Someone collided!@!!!!!!");
        if (collision.gameObject.tag == "Controller")
        {
            Debug.Log("And it is controller");
            text.text = "Big Plane Mode";
            // if plane is big right now
            if (!planeToResize.GetComponent<Resizable>().isLittle)
            {

                if (!planeToResize.GetComponent<Resizable>().isImmune && !planeToResize.GetComponent<Resizable>().isGrabbed)
                {
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        planeToResize.GetComponent<Resizable>().ToggleImmune(true);
                        //foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                        StartCoroutine(WaitAndResizeDown(planeToResize));
                    }
                }
            }

            // if the plane is already little
            else
            {
                if (!planeToResize.GetComponent<Resizable>().isImmune && !planeToResize.GetComponent<Resizable>().isGrabbed)
                {

                    text.text = "Toy Plane Mode";
                    if (!isExecuting)
                    {
                        isExecuting = true;
                        planeToResize.GetComponent<Resizable>().ToggleImmune(true);
                        //foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
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


    private void OnTriggerExit(Collider other)
    {
        text.color = tempColor;
    }
    IEnumerator WaitAndResizeDown(GameObject hittedObj)
    {
        GameObject tempRibbon = (GameObject)Instantiate(myRibbon, hittedObj.transform.position, hittedObj.transform.localRotation);

        // the safty check in case the ball is destroyed before it finish its duty
        hittedObj.GetComponent<Resizable>().DoSaftyCheck(tempRibbon);

        yield return new WaitForSeconds(1f);
        hittedObj.GetComponent<Resizable>().ResizeDown(spawnLoc);
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
