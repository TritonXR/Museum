using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour
{

    public AudioClip engineIdleSound;
    public AudioClip engineRunSound;
    public AudioClip landingSound;
    public AudioClip engineStartSound;


    private AudioSource engineSounds;
    //private AudioSource m_EngineRun;
    //private AudioSource m_Landing;
    //private AudioSource m_EngineStart;

    float MAX_DISTANCE = 35.0f;
    float MIN_DISTANCE = 5.0f;

    void Start()
    {
        engineSounds = this.gameObject.AddComponent<AudioSource>();
        engineSounds.spatialBlend = 1.0f;
        engineSounds.maxDistance = MAX_DISTANCE;
        engineSounds.minDistance = MIN_DISTANCE;
        engineSounds.clip = engineIdleSound;
    }

    public void SwitchSound(AudioClip clipToChangeTo, bool loop)
    {
        if (engineSounds.isPlaying)
        {
            engineSounds.Stop();
        }

        engineSounds.clip = clipToChangeTo;
        engineSounds.loop = loop;
        engineSounds.Play();
    }

    public void PlayEngineIdle()
    {
        SwitchSound(engineIdleSound, true);

    }

    public void PlayEngineRun()
    {
        SwitchSound(engineRunSound, true);
    }

    public void PlayLanding()
    {
        SwitchSound(landingSound, false);
    }

    public void PlayEngineStart()
    {
        StartCoroutine(PlayTwoClips(engineStartSound, engineIdleSound));
    }

    /// <summary>
    /// Play two sounds, the first doesn't loop and leads to the second sound that loops.
    /// </summary>
    /// <param name="engineStartSound"></param>
    /// <param name="engineIdleSound"></param>
    /// <returns></returns>
    IEnumerator PlayTwoClips(AudioClip firstClip, AudioClip secondClip)
    {
        SwitchSound(firstClip, false);

        while (engineSounds.isPlaying)
        {
            yield return null;
        }

        SwitchSound(secondClip, true);
    }
}
