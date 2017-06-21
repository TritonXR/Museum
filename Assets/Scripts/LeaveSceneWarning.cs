using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSceneWarning : MonoBehaviour {

    public Canvas warningSign;
    private float time;

    void OnEnable () {
        time = 0f;
	}

    private void Update()
    {
        time += Time.deltaTime;
        Debug.Log("Warning Script Working!");
    }
    
    private void OnTriggerEnter (Collider col) {
        Debug.Log("Player has entered the warning zone in " + time + " seconds!");
        Debug.Log(col.name);
        if (col.name == "WarningCollider" && time < 30f)
        {
                warningSign.enabled = true;
            Debug.Log("Player has entered the warning zone in less than 30 seconds!");
        }
	}

    private void OnTriggerExit(Collider col)
    {
        if (col.name == "WarningCollider" && warningSign.isActiveAndEnabled)
        {
                warningSign.enabled = false;
                time = 0f; //Resets time to make sure they look at Museum
            Debug.Log("Player has left the warning zone!");
        }
    }
}
