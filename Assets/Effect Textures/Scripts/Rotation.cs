using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	public float speed = 2;
	public Vector3 direction = new Vector3(0,1,0);
	void Update () {
		transform.Rotate( direction * speed * Time.deltaTime);
	}
}
