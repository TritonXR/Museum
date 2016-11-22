using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ManagerTest : MonoBehaviour
{

    private UnityAction someListener;

    void Awake()
    {
        someListener = new UnityAction(SomeFunction);
    }

    void OnEnable()
    {
        TutorialManager.StartListening("test", someListener);
        TutorialManager.StartListening("Spawn", SomeOtherFunction);
        TutorialManager.StartListening("Destroy", SomeThirdFunction);
    }

    void OnDisable()
    {
        TutorialManager.StopListening("test", someListener);
        TutorialManager.StopListening("Spawn", SomeOtherFunction);
        TutorialManager.StopListening("Destroy", SomeThirdFunction);
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }

    void SomeOtherFunction()
    {
        Debug.Log("Some Other Function was called!");
    }

    void SomeThirdFunction()
    {
        Debug.Log("Some Third Function was called!");
    }
}