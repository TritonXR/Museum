using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignActiveState : MonoBehaviour
{
    public GameObject sign;

    public void OnEnable()
    {
        sign.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.name=="Camera (Head)")
        {
            Debug.Log("You have entered the active zone");
            sign.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider col)
    {
        sign.SetActive(false);
    }

}
