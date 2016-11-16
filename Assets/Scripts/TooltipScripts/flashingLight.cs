using UnityEngine;
using System.Collections;



public class flashingLight : MonoBehaviour {
    public bool iMLOn = true;
    public Light[] childLight;
    public int lightOff = 0;
    public int lightOn = 8;
    public double counter = 1.0;
    public double counterMod = 1.0;

    // Use this for initialization
    void Start () {
        //Get the light child component
        childLight = GetComponentsInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        //childLight.SetActive(!iMLOn);

        if (counter % counterMod == 0.0)
        {
            if (iMLOn)
            {
                childLight[0].intensity = lightOff;
                childLight[1].intensity = lightOn;
            }
            else
            {
                childLight[0].intensity = lightOn;
                childLight[1].intensity = lightOff;
            }
            iMLOn = !iMLOn;
        }
        counter++;
    }
}

