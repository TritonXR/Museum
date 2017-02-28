using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeMe : MonoBehaviour {
    public GameObject player;
    public GameObject CameraEye;
    public GameObject text;
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

        text.SetActive(false);

        StartCoroutine(CheckingResize());


	}

    IEnumerator CheckingResize() {
        while(true) {
            if (rightDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (!text.activeSelf)
                {
                    text.SetActive(true);
                }

               // Debug.Log("GetPressDown");
                Resize();
            }
            else {

                if (text.activeSelf)
                {
                    text.SetActive(false);
                }
            }

            yield return null;
        }
    }

    void Resize()
    {
        float difference = rightDevice.GetAxis().y; // - CameraEye.transform.position.y;

        //Debug.Log(rightDevice.GetAxis().y + "  " + CameraEye.transform.position.y );
        
        if (difference > 0)
        {
            if (currSize < 1 && currSize > 0.1)
            {
                currSize = currSize + 0.01f;
            }
            else if (currSize >= 1)
            {
                currSize = currSize + 0.1f;
            }
            else
            {
                currSize = 0.11f;
            }
        }
        else if(difference < 0) {
            if (currSize <= 1 && currSize > 0.1)
            {
                currSize = currSize - 0.01f;
            }
            else if(currSize > 1) {
                currSize = currSize - 0.1f;
            }
            else {
                currSize = 0.11f;
            }
        }
        else {
            
        }

        //Debug.LogFormat("prevSize is: {0}", prevSize);
        //Debug.LogFormat("currSize is: {0}", currSize);

        //Vector3 tempPlayer = player.transform.position;
        player.transform.localScale = new Vector3(currSize, currSize, currSize);
        //player.transform.position = new Vector3(tempPlayer.x, currSize/2, tempPlayer.z);
        
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
