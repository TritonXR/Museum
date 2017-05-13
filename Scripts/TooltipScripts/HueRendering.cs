using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueRendering : MonoBehaviour {

    public Color hueToSet;

	// Use this for initialization
	void Start () {
        AdjustingHue(transform, hueToSet);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void AdjustingHue(Transform myTransform, Color color)
    {
        foreach (Transform child in myTransform)
        {
            if(child.gameObject.tag == "infobox")
            {
                continue;
            }
            AdjustingHue(child, color);

            Renderer rend = child.GetComponent<Renderer>();
            if (rend != null)
            {
                foreach (Material mat in rend.materials)
                {
                    mat.SetColor("_Color", color);
                }
            }
        }
    }
}
