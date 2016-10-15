using UnityEngine;
using System.Collections;

public class Groundctl : MonoBehaviour {
    bool beginAction = false;
    bool endAction = false;

	// Use this for initialization
	void Start () {
        Debug.Log("Begin");
        StartCoroutine(ActionTimer());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter(Collision collision)
    {
        beginAction = true;
        Debug.Log(beginAction);
        ActionTimer();

    }

    IEnumerator ActionTimer()
    {
        yield return new WaitForSeconds(5.0f); //if 5 seconds passes before action is made, reset       
        beginAction = false;
        Debug.Log(beginAction);

    }

    //If object has this script, set a boolean to true
    //Coroutine to deactivate boolean 
}
