using UnityEngine;
using System.Collections;

public class ConditionTester : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("CallingPlane");
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.ResetTrigger("CallingPlane");
        }
    }
}
