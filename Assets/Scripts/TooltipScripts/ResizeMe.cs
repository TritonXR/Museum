using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMe : MonoBehaviour {
    public GameObject player;
    //public SteamVR_Camera head;
    public float maximumDiff;

    private float currentDiff;
    private float currentSize;
    private float currentHeight;
    //private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device rightDevice;

	// Use this for initialization
	void Start () {
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        rightDevice = SteamVR_Controller.Input(rightIndex);

        StartCoroutine(CheckingResize());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CheckingResize()
    {
        while (true)
        {
            currentHeight = rightDevice.GetAxis().y;
            while (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){
                Debug.Log("Touch Pad is Pressed");
                currentSize = player.transform.localScale.x;
                currentDiff = rightDevice.GetAxis().y - currentHeight;
                currentSize += currentDiff;

                if (currentSize > 0)
                {
                    player.transform.localScale = new Vector3(currentSize, currentSize, currentSize);
                }

                yield return null;
            }
            yield return null;
        }

    }
}
