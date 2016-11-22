using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;


public class TutorialManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent> eventDictionary;

    private static TutorialManager tutorialManager;

    public static TutorialManager Instance
    {
        get
        {
            if (!tutorialManager)
            {
                tutorialManager = FindObjectOfType(typeof(TutorialManager)) as TutorialManager;

                if (!tutorialManager)
                {
                    Debug.LogError("There needs to be one active TutorialManager script on a GameObject in your scene");
                }
                else
                {
                    tutorialManager.Init();
                }
            }

            return tutorialManager;
        }
    }

    void Init()
    {
        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (tutorialManager == null) return;
        UnityEvent thisEvent = null;

        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}