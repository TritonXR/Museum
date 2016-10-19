using UnityEngine;
using System.Collections;




//TODO: Implement proper rotation lerping
//If atPlayer is true for an object and player clicks on another object, send current object back and new one forwards

public class Groundctl : MonoBehaviour {

    private bool atPlayer = false;

    public Vector3 startPos; //Beginning position of the lerp
    public Vector3 endPos; //Ending position of the lerp
    static public float dur = 3.0f; //duration of the lerp

	void Start () {
        Debug.Log("Begin");
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
        }
        else //For lerping back to originial position
        {
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
