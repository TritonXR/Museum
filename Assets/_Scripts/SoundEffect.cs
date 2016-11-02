using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour
{

    public AudioClip TakeOffSound;
    public AudioClip EngineSound;
    public AudioClip StartSound;


    private AudioSource m_TakeOff;
    private AudioSource m_Engine;
    private AudioSource m_Start;

    void Start()
    {
        m_TakeOff = this.gameObject.AddComponent<AudioSource>();
        m_TakeOff.clip = TakeOffSound;

        m_Engine = this.gameObject.AddComponent<AudioSource>();
        m_Engine.clip = EngineSound;

        m_Start = this.gameObject.AddComponent<AudioSource>();
        m_Start.clip = StartSound;

    }

    public void PlayTakeOff()
    {
        
        m_TakeOff.Play();
    }

    public void PlayEngine()
    {
        m_Engine.loop = true;
        m_Engine.Play();
    }
}
