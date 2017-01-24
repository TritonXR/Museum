using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HalfScale : MonoBehaviour {

    public Scrollbar bar;
    private float currScale;

	// Use this for initialization
	void Start () {
        bar.value = (transform.localScale.x - 0.01f)/(0.19f); 
	}
	
    IEnumerator ScalingPlane()
    {
        while (true)
        {
            currScale = bar.value * 0.19f + 0.01f;
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
