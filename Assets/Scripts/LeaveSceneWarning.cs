using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSceneWarning : MonoBehaviour {

    public GameObject Part2Sign;
    private float time;

    void Start () {
        time = 0f;
        Part2Sign.SetActive(false);
	}
    public float timeThreshold;
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= timeThreshold)
        {
            Debug.Log("Part 2 Sign is now Enabled!");
            Part2Sign.SetActive(true);
        }
    }
}
