using UnityEngine;
using System.Collections;

public class signalPlane : MonoBehaviour
{
    Ray gaze;
    bool signal;
    bool initMovement;



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
        //signal = true;
    }


    //needs to be tested
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<controlPoint>())
        {
            int ptNum = collision.collider.gameObject.GetComponent<controlPoint>().syncNum;
            if (ptNum == 1)
            {
                initMovement = true; //begins the controller movement
            }
            else if (ptNum == 2 && initMovement == true)
            {
                signal = true; //ends the controller movement, signals the plane
                initMovement = false; 

            }
        }
    }
}