using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

    
    public AudioClip introAudio;

    private bool PickedUpBaton;
    private AudioSource intro;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator GeneralTutorials()
    {
        yield return Intro();
        yield return Part1();
    }

    IEnumerator Intro()
    {
        while (!PickedUpBaton)
        {
            yield return new WaitForSeconds(0);
        }
        yield return null;
    }

    IEnumerator Part1()
    {

    }
}
