
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevateMe : MonoBehaviour
{
    public GameObject player;
    public GameObject text;

    private float currHeight;


    private SteamVR_Controller.Device leftDevice;

    // Use this for initialization
    void Start()
    {
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        leftDevice = SteamVR_Controller.Input(rightIndex);
        currHeight = player.transform.position.y;
        text.SetActive(false);

        StartCoroutine(ChangingHeight());

        //currentV = player.transform.localScale;
    }


    IEnumerator ChangingHeight()
    {
        while (true)
        {
            if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (!text.activeSelf)
                {
                    text.SetActive(true);
                }

                Debug.Log("GetPressDown");
                Elevate();

            }
            else
            {

                if (text.activeSelf)
                {
                    text.SetActive(false);
                }
            }

            yield return null;
        }
    }

    void Elevate()
    {
        float difference = leftDevice.GetAxis().y;


        if (difference > 0)
        {
            currHeight = currHeight + 0.05f;
        }
        else if (difference < 0)
        {
            currHeight = currHeight - 0.05f;
        }


        player.transform.position = new Vector3(player.transform.position.x, currHeight, player.transform.position.z);

    }
}

