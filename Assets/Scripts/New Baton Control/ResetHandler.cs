using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHandler : MonoBehaviour {

    public GameObject explosion;
    Vector3 planeResetLoc;
    Quaternion planeResetRot;

	// Use this for initialization
	void Start () {
        planeResetLoc = transform.position;
        planeResetRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IsObstacle>()) {
            reset();
        }
    }

    public void reset() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        transform.position = planeResetLoc;
        transform.rotation = planeResetRot;
        BatonHandler.instance.StopAllCoroutines();
        BatonHandler.instance.deactivateAllInput();
    }
   
}
