using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMe : MonoBehaviour {
    public GameObject player;
    public GameObject eye;
    //public SteamVR_Camera head;
    public float maximumDiff;

    private float currSize;
    private float currentHeight;
    private float prevSize;
    private Vector3 currentV;
    //private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device rightDevice;

	// Use this for initialization
	void Start () {
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        rightDevice = SteamVR_Controller.Input(rightIndex);

        currentV = player.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){
            prevSize = player.transform.localScale.x;
            currentHeight = rightDevice.GetAxis().y;
        }

        if (rightDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Resize();
        }

        if (rightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            prevSize = player.transform.localScale.x;


        }

    }

    //IEnumerator CheckingResize()
    //{
    //    Debug.Log("Get in to the Resize function");
    //    while (true)
    //    {
    //        currentHeight = player.transform.position.y;
    //        while (rightDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)){
    //            //Debug.Log("Touch Pad is Pressed");
    //            currentDiff = rightDevice.GetAxis().y - currentHeight ;
    //            //Debug.LogFormat("Yaxis of right controller is: {0}", rightDevice.GetAxis().y);
    //            currentSize = originalSize * currentDiff;


    //            //if (currentSize > 0)
    //            //{
    //            currentV.x = currentSize;
    //            currentV.y = currentSize;
    //            currentV.z = currentSize;
    //            player.transform.localScale = currentV;
    //            //}

    //            yield return null;
    //        }
    //        yield return null;
    //    }

    //}

    void Resize()
    {
        float updatedHeight = rightDevice.GetAxis().y;
        currSize = 5 * ((updatedHeight - currentHeight) / currentHeight) * prevSize;
        player.transform.localScale = new Vector3(currSize, currSize, currSize);
    }
}
