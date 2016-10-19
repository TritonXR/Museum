using UnityEngine;
using System.Collections;




/*TODO: 
 * Implement proper rotation lerping
 * If object is clicked while lerping, move back. 
 * If atPlayer is true for an object and player clicks on another object, send current object back and new one forwards
 * 
 * BUGS: Name of hit gameobject prints three times
 * Line 67 (do more stuff here) doesn't print
 */

public class Groundctl : MonoBehaviour {

    private bool atPlayer = false;
    string objAtPlayer = "";
    RaycastHit hit;

    public Vector3 startPos; //Beginning position of the lerp
    public Vector3 endPos; //Ending position of the lerp
    static public float dur = 3.0f; //duration of the lerp
    

	void Start () {
        Debug.Log("Begin");
	}


    void Update()
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
        //Info for the lerp:
        //Duration time: 3 seconds
        if (!atPlayer) //For the lerping towards the player 
        {
            for (float i = 0; i < dur; i += Time.deltaTime)
            {
                Vector3 newPos = Vector3.Lerp(startPos, endPos, i / dur);
                this.transform.position = newPos;
                yield return null;
            }
            this.transform.position = endPos;
            atPlayer = true;
            objAtPlayer = hit.transform.gameObject.name;
        }
        else //For lerping back to originial position
        {
            if(objAtPlayer != hit.transform.gameObject.name)
            {
                Debug.Log("Insert More Stuff Here Later");
            }

            for (float i = 0; i < dur; i += Time.deltaTime)
            {
                Vector3 newPos = Vector3.Lerp(endPos, startPos, i / dur);
                this.transform.position = newPos;
                yield return null;
            }
            this.transform.position = startPos;
            atPlayer = false;
        }
    }

    public void OnMouseDown()
    {

        StartCoroutine(MovePlane());

    }

    //If object has this script, set a boolean to true
    //Coroutine to deactivate boolean 
}
