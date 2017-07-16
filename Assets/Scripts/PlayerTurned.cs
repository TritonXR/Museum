using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurned : MonoBehaviour {
    //Script used for Instructions Sign
    public GameObject Instructions;

	void Start () {
        Debug.Log("["+ DateTime.Now.ToString("h:mm:ss") + "] Instructions sign is active.");
        Instructions.SetActive(true);
        StartCoroutine(Timer());
       
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        Instructions.SetActive(false);
        Debug.Log("[" + DateTime.Now.ToString("h:mm:ss") + "] Instructions sign is inactive.");
        yield return null;
    }
}
