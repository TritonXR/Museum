using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ModelActiveState : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
	// Use this for initialization
	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
	}

    private void OnTriggerStay(Collider col)
    {
        if((col.tag == "resizable" || col.tag == "baton") && device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = false;
        }
        else if(trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled == false && !device.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if((col.tag == "resizable" || col.tag == "baton"))
        trackedObj.GetComponentInChildren<SteamVR_RenderModel>().enabled = true;
    }
}
