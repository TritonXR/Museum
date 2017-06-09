using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAdvancer : MonoBehaviour {

    public string nextScene;
    private string thisScene; 
	// Use this for initialization
	void Awake () {
        thisScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("right"))
        {
            Debug.Log("Loading " + nextScene);
            SceneManager.LoadScene(nextScene);
        }
        else if (Input.GetKeyDown("r"))
        {
            Debug.Log("Reloading " + thisScene);
            SceneManager.LoadScene(thisScene);
        }
	}
}
