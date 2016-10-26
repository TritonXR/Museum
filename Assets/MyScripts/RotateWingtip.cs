using UnityEngine;
using System.Collections;

public class RotateWingtip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public Transform from;
    public Transform to;

    public static Vector3 start = new Vector3(2.924f, 18.908f, -5.062f);

    private Quaternion startR = Quaternion.Euler(start);

    public static Vector3 end = new Vector3(18.482f, 17.416f, -5.333f);
    public float speed = 0.5f;
    private Quaternion endR = Quaternion.Euler(end);
	// Update is called once per frame

	void Update () {
        transform.rotation = Quaternion.Lerp(startR, endR, Time.time * speed);
	}
}
