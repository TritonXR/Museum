using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalfScale : MonoBehaviour {

    public Scrollbar bar;
    private float currScale;
    private float minScale;
    private float maxScale;


	// Use this for initialization
	void Start () {
        minScale = transform.localScale.x * 0.05f;
        maxScale = transform.localScale.x * 5f; 
        bar.value = (transform.localScale.x - minScale)/(maxScale - minScale);
        StartCoroutine(ScalingPlane());
	}
	
    IEnumerator ScalingPlane()
    {
        while (true)
        {
            currScale = bar.value * (maxScale - minScale) + minScale;
            transform.localScale = new Vector3(currScale, currScale, currScale);
            yield return null;
        }
    }































	// Update is called once per frame
	void Update () {

// uncomment to scale by keyboard
/*        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(transform.localScale.x < 0.002)
            {
                
            }
            if (transform.localScale.x < 0.02)
            {
                transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            }
            else
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (transform.localScale.x < 0.02)
            {
                transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            }
            else
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

*/

    }
}
