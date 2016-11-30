using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

    
    public AudioClip introAudio;   // Welcome to ...
    public AudioClip part1Audio;   // Now, check out the person next you ....
    public AudioClip part2Audio;   // Now, look at any plane and do gesture 1 to let plane move toward you...
    public AudioClip part3Audio;   // Nice work, now try to moving your self around by teleporting... 
    public AudioClip endingAudio;  // Good job, you've completed the tutorial, now you can play around ....
    public AudioClip congratsAudio; // Nice Job, Well done...


    private bool pickedUpBaton;
    private bool doneGesture1;
    private AudioSource currSound;

	// Use this for initialization
	void Start () {
        // set up the welcome instruction
        currSound = this.gameObject.AddComponent<AudioSource>();
        //currSound.sptaialBlend = 1.0f;

        StartCoroutine(GeneralTutorials());

        // exit tutorial mode and enable game mode.
    }
	
    IEnumerator GeneralTutorials()
    {
        Debug.Log("Start!!!");
        yield return Intro();
        Debug.Log("Intro ends...");
        yield return Part1();
        Debug.Log("Part1 ends...");
        yield return Part2();
        Debug.Log("Part2 ends...");
        yield return Part3();
        Debug.Log("Part3 ends...");
        PlayAudio(endingAudio);
        Debug.Log("Everthing ends...");


        yield return null;
    }

    IEnumerator Intro()
    {
        currSound.clip = introAudio;
        currSound.Play();

        // Baton start glowing

        yield return CheckPickingUpBaton();

        // Baton stop glowing

        yield return null;
    }

    IEnumerator Part1()
    {
        Debug.Log("Doing Part1... ");
        PlayAudio(part1Audio);

        // call the animation method to let character model start waving baton gesture 1.
        // to delete:
        yield return new WaitForSeconds(2f);

        yield return CheckPart1Gesture();

        Congrats();
        // should wait until congrats audio finished.
        yield return new WaitForSeconds(2f);

        yield return null;

    }

    IEnumerator Part2()
    {
        Debug.Log("Doing Part2... ");
        PlayAudio(part2Audio);

        // to delete:
        yield return new WaitForSeconds(2f);

        yield return Gesture1Practice();

        Congrats();
        // should wait until congrats audio finished.
        yield return new WaitForSeconds(2f);

        yield return null;
    }

    IEnumerator Part3()
    {
        Debug.Log("Doing Part3... ");

        PlayAudio(part3Audio);

        // to delete:
        yield return new WaitForSeconds(2f);

        yield return CheckTeleport();

        Congrats();        

        // should wait until congrats audio finished.
        yield return new WaitForSeconds(2f);

        yield return null;
    }

    IEnumerator CheckPickingUpBaton()
    {
        // TODO: loop while not user not picking up baton

        // to delete:
        yield return new WaitForSeconds(2f);
        yield return null;
    }

    IEnumerator CheckPart1Gesture()
    {
        // start checking gesture instructed by Part1Audio
        // TODO: loop while not checked.

        // to delete:
        Debug.Log("Checking Gesture 1...");
        yield return new WaitForSeconds(2f);
        doneGesture1 = true;

        yield return null;

    }

    IEnumerator Gesture1Practice()
    {
        // TODO: loop while not true
        // set to true when did gesture right and after plane moved to correct position.

        // to delete:
        Debug.Log("Gesture 1 Practice... ");
        yield return new WaitForSeconds(2f);

        yield return null;
    }

    IEnumerator CheckTeleport()
    {
        // TODO: loop while user not complete one teleport action

        // to delete:
        Debug.Log("Checking Teleport... ");
        yield return new WaitForSeconds(2f);

        yield return null;
    }

    void PlayAudio(AudioClip currClip)
    {
        currSound.Stop();
        currSound.clip = currClip;
        currSound.Play();
    }

    void Congrats()
    {
        Debug.Log("Congrats!");

        currSound.Stop();
        currSound.clip = congratsAudio;
        currSound.Play();
    }
}
