using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ModelActiveState : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    // Use this for initialization

    public GameObject model;
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
	}

    private void OnTriggerStay(Collider col)
    {
        if((col.tag == "resizable" || col.tag == "baton") && device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = false;
            model.SetActive(false);
        }
        else if(model.activeSelf == false && !device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            //trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = true;
            model.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.tag == "resizable" || col.tag == "baton") && model.activeSelf == false)
            //trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = true;
            model.SetActive(true);
    }
}
