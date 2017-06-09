using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HeadPosLog : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    public GameObject head;
    // Use this for initialization
    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if(device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("Head Rotation 1: " + head.transform.rotation.y);
        }
        else if(device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_A))
        {
            Debug.Log("Head Roration 2: " + head.transform.rotation.y);
        }
    }
}
