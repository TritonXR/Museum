using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(SteamVR_TrackedObject))]

public class ControlsAppearScript : MonoBehaviour {

    public GameObject controlsBubble;
    public GameObject triggerLink;
    public GameObject touchpadLink;
    private bool isVisible;
    SteamVR_TrackedObject trackObj;
    SteamVR_Controller.Device device;

    private void Awake()
    {
        trackObj = GetComponent<SteamVR_TrackedObject>();
        isVisible = true;
    }

    // Use this or initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackObj.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) ||
            device.GetPress(SteamVR_Controller.ButtonMask.Trigger) || 
            Input.GetKeyUp(KeyCode.X))
        {
            //nothing
        }

    }

    // Update is called once per frame
    void Update () {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) ||
            device.GetPress(SteamVR_Controller.ButtonMask.Trigger) ||
            Input.GetKeyUp(KeyCode.X))
        {
            isVisible = false;
            // adjust scale and position of bubbles after initially used
            Vector3 scaleBubble = new Vector3(1f, 1f, 1f);
            Vector3 positionBubble = new Vector3(-0.12f, 0.001f, -0.061f);

            controlsBubble.transform.localPosition = positionBubble;
            controlsBubble.transform.localScale = scaleBubble;

            Vector3 positionTouchpadLink = new Vector3(0.0921f, 0.0127f, -0.0018f);
            Vector2 sizeDeltaTouchpadLink = new Vector2(1.41f, 0.1f);

            Vector3 positionTriggerLink = new Vector3(0.1449f, 0.0136f, 0.0283f);
            Vector2 sizeDeltaTriggerLink = new Vector2(3.2f, 0.1f);
            touchpadLink.GetComponent<RectTransform>().localPosition = positionTouchpadLink;
            touchpadLink.GetComponent<RectTransform>().sizeDelta = sizeDeltaTouchpadLink;
            //0f, -4.51f, 2.51f
            triggerLink.transform.localPosition = positionTriggerLink;
            triggerLink.GetComponent<RectTransform>().sizeDelta = sizeDeltaTriggerLink;
        }
        else
        {
            isVisible = true;
        }

        controlsBubble.SetActive(isVisible);
    }
}
