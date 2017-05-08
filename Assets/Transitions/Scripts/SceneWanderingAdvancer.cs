using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWanderingAdvancer : MonoBehaviour {

    public string nextScene;
    public GameObject baton1, baton2;

    private Vector3 pos1, pos2;
    // Use this for initialization
    void Awake()
    {
        pos1 = baton1.GetComponent<Rigidbody>().transform.position;
        pos2 = baton2.GetComponent<Rigidbody>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos1 != baton1.GetComponent<Rigidbody>().transform.position && pos2 != baton2.GetComponent<Rigidbody>().transform.position) //Checks to see if both batons have been moved
        {
            Debug.Log("Loading " + nextScene);
            SceneManager.LoadScene(nextScene);
        } else if(pos1 != baton1.GetComponent<Rigidbody>().transform.position || pos2 != baton2.GetComponent<Rigidbody>().transform.position)
        {
            pos1 = baton1.GetComponent<Rigidbody>().transform.position;
            pos2 = baton2.GetComponent<Rigidbody>().transform.position;
        }
    }
}
