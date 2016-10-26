using UnityEngine;
using System.Collections;

public class signalPlane : MonoBehaviour
{
    Ray gaze;
    bool signal;

    // Use this for initialization
    void Start()
    {
        signal = false;
    }

    // Update is called once per frame
    void Update()
    {
        gaze = new Ray(Singleton.instance.player.transform.position, Singleton.instance.player.transform.forward); //the ray that comes out of the camera
        RaycastHit plane2move; //access to the object that we're looking at
        if (Physics.Raycast(gaze, out plane2move, Mathf.Infinity)) //actually casting the ray
        {
            if (plane2move.collider.gameObject.GetComponent<directPlane>()) //if the object has the directPlane component
            {
                //highlight plane here
                if (signal || Input.GetKeyDown(KeyCode.S))
                {
                    plane2move.collider.gameObject.GetComponent<directPlane>().Activate();
                }
            }
        }
    }

    public void OnMouseDown()
    {
        signal = true;
    }

}