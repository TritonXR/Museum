using UnityEngine;
using System.Collections;

public class Scales : MonoBehaviour {
	public float speed_ = 2;
	public float scaleMax_ = 3;
	private Vector3 prevScale ;
	private float MaxScaleX;
	void Start(){
		prevScale = transform.localScale;
		MaxScaleX = prevScale.x + scaleMax_;
	}
	void Update () {
		if(transform.localScale.x < MaxScaleX){
			transform.localScale += new Vector3(1,1,1) * speed_ * Time.deltaTime;
		}else{
			transform.localScale = prevScale;
		}
		//Debug.Log("MaxScaleX = "+MaxScaleX+"; transform.localScale="+transform.localScale.x);
	}
}
