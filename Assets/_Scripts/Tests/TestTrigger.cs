using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            TutorialManager.TriggerEvent("test");
        }

        if (Input.GetKeyDown("v"))
        {
            TutorialManager.TriggerEvent("Spawn");
        }

        if (Input.GetKeyDown("b"))
        {
            TutorialManager.TriggerEvent("Destroy");
        }

        if (Input.GetKeyDown("n"))
        {
            TutorialManager.TriggerEvent("Junk");
        }
    }
}