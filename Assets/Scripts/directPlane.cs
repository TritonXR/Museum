using UnityEngine;
using System.Collections;
using VRTK;




/*TODO: 
 * Implement proper rotation lerping
 * If object is clicked while lerping, move back. 
 * If atPlayer is true for an object and player clicks on another object, send current object back and new one forwards
 * 
 * BUGS: Name of hit gameobject prints three times
 * Line 67 (do more stuff here) doesn't print
 * 
 * 
 */

public class directPlane : VRTK_InteractableObject {

    private bool atPlayer = false;
    RaycastHit hit;
    bool running;

    Vector3 startPos; //Beginning position of the lerp
    Vector3 endPos; 
    Quaternion startRot;
    Quaternion endRot;
    static public float dur = 3.0f; //duration of the lerp
    
    

	protected override void Start () {
        base.Start();
        startRot = transform.rotation;
        startPos = transform.position;
    }


    protected override void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
    
    //For movement of the object on click
    public IEnumerator MovePlane()
    {
        int forwardMult = 7;
        running = true;

        if (!atPlayer) //For the lerping towards the player 
        {
            startPos = transform.position; //start position at player
            endPos = Singleton.instance.player.transform.position + (Singleton.instance.player.transform.forward * forwardMult); //end position at player
            endPos.y = startPos.y; //lock movement on the y axis
            startRot = transform.rotation;
            transform.LookAt(Singleton.instance.player.transform); //points the plane in the direction of the player
            yield return StartCoroutine(lerpPlaneRot(transform.rotation)); //lerping rotation from the start (inizialized at start) to the end position (lookAt)
            yield return StartCoroutine(lerpPlaneTrans(endPos));
            this.transform.position = endPos; //snap to end position
            atPlayer = true; //plane is now at player
        }
        else //For lerping back to originial position
        {
            endPos = startPos; //set end position to original idle position 
            startPos = transform.position; //setting the start position to the current position
            endRot = startRot;
            startRot = transform.rotation;
            yield return StartCoroutine(lerpPlaneTrans(endPos)); //lerp
            yield return StartCoroutine(lerpPlaneRot(endRot));
            this.transform.position = endPos; //snap to end
            atPlayer = false; //no plane at player
        }
       
        running = false;
    }

    //Lerping the plane
    IEnumerator lerpPlaneRot(Quaternion toEndRot)
    {
        //Rotation of the plane
        //Locks the X axis for the rotation
        Quaternion endRot = transform.rotation;
        Vector3 endRotV3 =  toEndRot.eulerAngles;
        endRotV3.x = startRot.eulerAngles.x; 
        toEndRot = Quaternion.Euler(endRotV3);

        for (float i = 0; i < dur; i += Time.deltaTime)
        {
            Quaternion newRot = Quaternion.Lerp(startRot, toEndRot, i / dur);
            this.transform.rotation = newRot;
            yield return null;
        }
        //Translation of the plane
    }

    private IEnumerator lerpPlaneTrans(Vector3 toEndPos)
    {
        for (float j = 0; j < dur; j += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(startPos, toEndPos, j / dur);
            this.transform.position = newPos;
            yield return null;
        }

    }

    public void OnMouseDown()
    {
        Activate();
    }

    public void Activate()
    {        
        if (!running)
        {

            if(Singleton.instance.plane == null) //if there's no saved plane
            {
                Singleton.instance.plane = transform.gameObject; //Set this object as the saved plane
                StartCoroutine(MovePlane()); //Move this plane
            }
            else if (Singleton.instance.plane != transform.gameObject)
            {
                StartCoroutine(Singleton.instance.plane.GetComponent<directPlane>().MovePlane());
                Singleton.instance.plane = transform.gameObject;//save this object in the singleton
                StartCoroutine(MovePlane()); //move this plane
            }
            else
            {
                StartCoroutine(MovePlane());
                Singleton.instance.plane = null;
            }
            //StartCoroutine(MovePlane());
        }
    }

    public override void StartUsing(GameObject usingObject)
    {
        Debug.Log("Plane Start");
        base.StartUsing(usingObject);
        Activate();
    }


    public override void StopUsing(GameObject usingObject)
    {
        Debug.Log("Plane Stop");
        base.StopUsing(usingObject);
        Activate();
    }
    //If object has this script, set a boolean to true
    //Coroutine to deactivate boolean 
}
