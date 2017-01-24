using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatonControl : MonoBehaviour
{

    public bool active;

    // Use this for initialization
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        active = true;
        Debug.LogFormat("{0} Entered", other.gameObject.name);
        BatonHandler.instance.checkCombinations();
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
        Debug.LogFormat("{0} Exited", other.gameObject.name);
    }
}
