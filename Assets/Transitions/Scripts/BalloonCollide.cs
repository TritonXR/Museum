using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonCollide : MonoBehaviour {

    public string nextScene;
    private float time;
    public double Delay;

    IEnumerator delay()
    {
        while (true)
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
    }
    private void OnTriggerEnter(Collider col)
    {
        StartCoroutine(delay());
        
    }
}
