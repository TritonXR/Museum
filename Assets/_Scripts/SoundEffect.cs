using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour
{

    public AudioClip EngineIdleSound;
    public AudioClip EngineRunSound;
    public AudioClip LandingSound;
    public AudioClip EngineStartSound;


    private AudioSource m_EngineIdle;
    private AudioSource m_EngineRun;
    private AudioSource m_Landing;
    //private AudioSource m_EngineStart;

    void Start()
    {
        m_EngineIdle = this.gameObject.AddComponent<AudioSource>();
        m_EngineIdle.clip = EngineIdleSound;

        m_EngineRun = this.gameObject.AddComponent<AudioSource>();
        m_EngineRun.clip = EngineRunSound;

        m_Landing = this.gameObject.AddComponent<AudioSource>();
        m_Landing.clip = LandingSound;

    }

    public void PlayEngineIdle()
    {
        if (m_EngineRun.isPlaying)
        {
            m_EngineRun.Stop();
        }

        m_EngineIdle.loop = true;
        m_EngineIdle.Play();
    }

    public void PlayEngineRun()
    {
        if (m_EngineIdle.isPlaying)
        {
            m_EngineIdle.Stop();

        }

        m_EngineRun.loop = true;
        m_EngineRun.Play();
    }

    public void PlayLanding()
    {
        m_Landing.Play();
    }
}
