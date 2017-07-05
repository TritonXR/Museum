using UnityEngine;
using System.Collections;

public class SMCHelper : MonoBehaviour {
	public Transform[] groups;
	
	void Awake(){
		if(groups!=null){
			if(groups.Length>0){
				SMCPlayer.groupsTransforms = groups;	
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.Alpha2)){
			SMCPlayer.PlayAnimationGroups(true);
		}
	}
}
