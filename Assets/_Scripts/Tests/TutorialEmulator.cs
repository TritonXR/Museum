using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TutorialEmulator : MonoBehaviour {

    private bool part1Finished;
    private UnityAction part1Listener;

	// Use this for initialization
	void Start () {
        part1Finished = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        part1Listener = new UnityAction(Part1);

    }

    void OnEnable()
    {
        TutorialManager.StartListening("part1",);
    }

    void StartPart1()
    {
        StartCoroutine(Part1());
    }

    IEnumerator Part1()
    {
        // wait for 5 seconds to let user get used to the virtual environment
        yield return new WaitForSeconds(5);

        // start playing first part tutorial.

        // check if the audio finished playing 
        // if so, then;
        part1Finished = true; 

        yield return null;
    }
}
