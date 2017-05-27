using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BalloonCollide : MonoBehaviour {

    public string nextScene;
    private float time;
    public double Delay;

    public GameObject Balloonmodel;
    public GameObject popText;
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
    private void OnTriggerEnter(Collider col)
    {
        Balloonmodel.SetActive(false);
        popText.SetActive(false);
        StartCoroutine(delay());
    }
}
