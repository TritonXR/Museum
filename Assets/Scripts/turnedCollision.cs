using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnedCollision : MonoBehaviour {

    public GameObject sign;

    private void OnTriggerStay(Collider col)
    {
        
        if(col.name == "turnedCol")
        {
            Debug.Log("Player Turned Around!");
            sign.SetActive(false);
            col.enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<PlayerTurned>().enabled = true;
        }
    }
}
