using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

    public static Singleton instance;
    public GameObject player;
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
}
