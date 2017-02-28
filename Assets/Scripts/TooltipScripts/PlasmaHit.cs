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

        Debug.Log("I am Plasma, I hitted the plane");

        if(collision.gameObject.tag == "resizable")
        {
            GameObject hittedObj = collision.gameObject;

            if (!hittedObj.GetComponent<Resizable>().isLittle)
            {
                if (!isExecuting)
                {
                    isExecuting = true;
                    foreach (Transform child in transform) { if (child.gameObject.activeSelf) child.gameObject.SetActive(false); }
                    StartCoroutine(WaitAndResize(hittedObj));
                }
            }
        }
    }

    IEnumerator WaitAndResize(GameObject hittedObj)
    {
        GameObject tempRibbon = (GameObject)Instantiate(myRibbon, hittedObj.transform.position, hittedObj.transform.localRotation);
        yield return new WaitForSeconds(1f);
        hittedObj.GetComponent<Resizable>().ResizeDown();
        yield return new WaitForSeconds(0.5f);
        Destroy(tempRibbon);

        yield return null;
    }

    
}
