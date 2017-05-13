using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdvancer : MonoBehaviour {

    public string nextScene;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("right"))
        {
            Debug.Log("Loading " + nextScene);
            SceneManager.LoadScene(nextScene);
        }
	}
}
