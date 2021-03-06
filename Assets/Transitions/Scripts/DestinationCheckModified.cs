﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCheckModified : MonoBehaviour {

    public PlaneCheck frontCheck;
    public PlaneCheck backCheck;

    public GameObject fireworks;

    public static DestinationCheckModified instance;
    public Material finishedMat;

    public GameObject transitionObject;
    public LevelChanger nextLevel;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        transitionObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkPlane(){
        if(frontCheck.active && backCheck.active)
        {
            BatonHandler.instance.StopAllCoroutines();
            BatonHandler.instance.deactivateAllInput();
            GetComponent<Renderer>().material = finishedMat;
            Debug.Log("Plane at destination!");
            Destroy(BatonHandler.instance.plane.GetComponent<IsPlane>());

            //Activate Next Level on Controller
            transitionObject.SetActive(true);

        }
    }
}
