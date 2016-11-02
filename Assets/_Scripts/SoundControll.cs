using UnityEngine;
using System.Collections;

public class SoundControll : MonoBehaviour {

    private SoundEffect sfx;

    // Use this for initialization
    void Start () {
        sfx = this.GetComponent<SoundEffect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sfx.PlayTakeOff();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            sfx.PlayEngine();
        }
	}
}
