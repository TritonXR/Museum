using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMe : MonoBehaviour {
    public GameObject player;
    public GameObject CameraEye;
    //public SteamVR_Camera head;
    //public float maximumDiff;

    private float currSize;
    private float currentHeight;
    private float prevSize;
    //private Vector3 currentV;
    //private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device rightDevice;

	// Use this for initialization
	void Start () {
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        rightDevice = SteamVR_Controller.Input(rightIndex);
        currSize = player.transform.localScale.x;
        StartCoroutine(CheckingResize());

        //currentV = player.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {



    }

    IEnumerator CheckingResize() {
        while(true) {
            if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Debug.Log("GetPressDown");
                Resize();

            }

            yield return null;
        }
    }

    void Resize()
    {
        float difference = rightDevice.GetAxis().y - CameraEye.transform.position.y;

        Debug.Log(rightDevice.GetAxis().y + "  " + CameraEye.transform.position.y );
        if (difference > 0)
        {
            if (currSize < 1 && currSize > 0)
            {
                currSize = currSize + 0.1f;
            }
            else if (currSize >= 1)
            {
                currSize = currSize + 1f;
            }
            else
            { 
                // currSize <= 0
            }
        }
        else if(difference < 0) {
            if (currSize <= 1 && currSize > 0)
            {
                currSize = currSize - 0.1f;
            }
            else if(currSize > 1) {
                currSize = currSize - 1f;
            }
            else {
                // currSize <= 0
            }
        }
        else {
            
        }

        Debug.LogFormat("prevSize is: {0}", prevSize);
        Debug.LogFormat("currSize is: {0}", currSize);


        player.transform.localScale = new Vector3(currSize, currSize, currSize);
        
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

}
