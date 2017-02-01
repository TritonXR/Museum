
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateMe : MonoBehaviour
{
    public GameObject player;

    private float currHeight;


    private SteamVR_Controller.Device leftDevice;

    // Use this for initialization
    void Start()
    {
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        leftDevice = SteamVR_Controller.Input(rightIndex);
        currHeight = player.transform.position.y;


        StartCoroutine(ChangingHeight());

        //currentV = player.transform.localScale;
    }


    IEnumerator ChangingHeight()
    {
        while (true)
        {
            if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                Debug.Log("GetPressDown");
                Elevate();

            }

            yield return null;
        }
    }

    void Elevate()
    {
        float difference = leftDevice.GetAxis().y;


        if (difference > 0)
        {
            currHeight = currHeight + 0.1f;
        }
        else if (difference < 0)
        {
            currHeight = currHeight - 0.1f;
        }


        player.transform.position = new Vector3(player.transform.position.x, currHeight, player.transform.position.z);

    }
}

