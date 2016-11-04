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

    static private bool atPlayer = false;
    string objAtPlayer = "";
    RaycastHit hit;
    bool running;

    Vector3 startPos; //Beginning position of the lerp
    Vector3 endPos; 
    Quaternion startRot;
    Quaternion endRot;
    static public float dur = 3.0f; //duration of the lerp
    

	protected override void Start () {
        base.Start();
        Debug.Log("Begin");
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
                objAtPlayer = hit.transform.gameObject.name;
            }
        }
    }
    
    //For movement of the object on click
    IEnumerator MovePlane()
    {
        int forwardMult = 5;
        running = true;
        
        if (!atPlayer) //For the lerping towards the player 
        {
            startPos = transform.position; //start position at player
            endPos = Singleton.instance.player.transform.position + (Singleton.instance.player.transform.forward * forwardMult); //end position at player
            endPos.y = startPos.y; //lock movement on the y axis

            startPos = transform.position;
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
            yield return StartCoroutine(lerpPlaneTrans(endPos)); //lerp

            endRot = startRot;
            startRot = transform.rotation;
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

    IEnumerator lerpPlaneTrans(Vector3 toEndPos)
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
        Debug.Log(running);

        if (!running)
        {
            StartCoroutine(MovePlane());
        }
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        Activate();
    }

    //If object has this script, set a boolean to true
    //Coroutine to deactivate boolean 
}
