using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonCollide : MonoBehaviour {

    public string nextScene;
    private float time;
    private double Delay = 5;

    public GameObject Balloonmodel;
    //public GameObject popText;

    public GameObject RedButton;
    IEnumerator delay()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time >= Delay)
            {
                //yield return new WaitForSeconds(Delay);
                RedButton.GetComponent<Animator>().enabled = false;
                Debug.Log("Loading " + nextScene);
                SceneManager.LoadScene(nextScene);
                yield return null;
            }
            else
                yield return null;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("The button has collided with " + col.name);
        if (col.name == "VisualLBaton" || col.name == "VisualRBaton")
        {
            RedButton.GetComponent<Animator>().enabled = true;
            //Balloonmodel.SetActive(false);
            //popText.SetActive(false);
            StartCoroutine(delay());
        }
    }
}
