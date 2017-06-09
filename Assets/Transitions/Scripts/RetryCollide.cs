using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryCollide : MonoBehaviour
{

    private string thisScene; 
    private float time;
    private double Delay = 1;

    public GameObject cyllindercol;
    //public GameObject popText;

    public GameObject RetryButton;
    IEnumerator delay()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time >= Delay)
            {
                //yield return new WaitForSeconds(Delay);
                RetryButton.GetComponent<Animator>().enabled = false;
                Debug.Log("Reloading " + thisScene);
                SceneManager.LoadScene(thisScene);
                yield return null;
            }
            else
                yield return null;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        thisScene = SceneManager.GetActiveScene().name;
        Debug.Log("The retry button has collided with " + col.name);
        if (col.name == "VisualLBaton" || col.name == "VisualRBaton")
        {
            RetryButton.GetComponent<Animator>().enabled = true;
            StartCoroutine(delay());
        }
    }
}
