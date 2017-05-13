using UnityEngine;
using System.Collections;

public class signalPlane : MonoBehaviour
{
    Ray gaze;
    bool signal;
    bool initMovement;

    GameObject plane1;


    // Use this for initialization
    void Start()
    {
        signal = false;
    }

    // Update is called once per frame
    void Update() //TODO: TEST PLANE MEMORY
    {
        /*
        gaze = new Ray(Singleton.instance.player.transform.position, Singleton.instance.player.transform.forward); //the ray that comes out of the camera
        RaycastHit planeToMove; //access to the object that we're looking at
        if (Physics.Raycast(gaze, out planeToMove, Mathf.Infinity)) //actually casting the ray
        {
            if (planeToMove.collider.gameObject.GetComponent<directPlane>()) //if the object has the directPlane component
            {
                if( plane1 == null || signal)
                {
                    plane1 = planeToMove.collider.gameObject; //storing the plane 
                    plane1.GetComponent<directPlane>().Activate();
                    Debug.Log(plane1.name);
                }
                else if (signal)
                {
                    Debug.Log(plane1.name);
                    plane1.GetComponent<directPlane>().Activate(); //activate plane to get it to move back 
                    plane1 = planeToMove.collider.gameObject; 
                    plane1.GetComponent<directPlane>().Activate();
                    Debug.Log(plane1.name);
                }
                */

        /*
        if (signal || Input.GetKeyDown(KeyCode.S))
        {
            planeToMove.collider.gameObject.GetComponent<directPlane>().Activate();
        }
        */
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