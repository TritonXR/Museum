using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonHandler : MonoBehaviour {
    public BatonControl LPanel;
    public BatonControl RPanel;

    public static BatonHandler instance;
    public static GameObject plane;

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
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void checkCombinations()
    {
        if(LPanel.active && RPanel.active)
        {
            Debug.Log("FORWARD");
        }
    }
}
