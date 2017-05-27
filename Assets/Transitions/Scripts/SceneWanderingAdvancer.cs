using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWanderingAdvancer : MonoBehaviour {

    public string nextScene;
    public GameObject baton1, baton2;
    public double Delay;
    private float time=0;
    private Vector3 pos1, pos2;
    // Use this for initialization
    void Awake()
    {
        pos1 = baton1.GetComponent<Rigidbody>().transform.position;
        pos2 = baton2.GetComponent<Rigidbody>().transform.position;
    }
    IEnumerator delay()
    {
        time += Time.deltaTime;
        if (time >= Delay)
        {
            //yield return new WaitForSeconds(Delay);
            Debug.Log("Loading " + nextScene);
            SceneManager.LoadScene(nextScene);
            yield return null;
        }
        else
            yield return null;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (pos1 != baton1.GetComponent<Rigidbody>().transform.position && pos2 != baton2.GetComponent<Rigidbody>().transform.position) //Checks to see if both batons have been moved
        {
            StartCoroutine(delay()); 
        } else if(pos1 != baton1.GetComponent<Rigidbody>().transform.position || pos2 != baton2.GetComponent<Rigidbody>().transform.position) //Checks which baton is being held
        {
            pos1 = baton1.GetComponent<Rigidbody>().transform.position;
            pos2 = baton2.GetComponent<Rigidbody>().transform.position;
        }
    }
}
