﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class BatonPickUp : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    private void OnTriggerStay(Collider col)
    {

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && (col.name == "groundcrew light stick" || col.name == "groundcrew light stick (1)") && col.attachedRigidbody.isKinematic == false)
        {
            Debug.Log("You have collided with " + col.name + " while holding down grip.");
            col.gameObject.transform.SetParent(gameObject.transform);
            col.attachedRigidbody.isKinematic = true;
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;
        }
    }
}
