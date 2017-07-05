using UnityEngine;
using System.Collections;

public class SMCPlayer : MonoBehaviour {
	public static Transform[] groupsTransforms = new Transform[0];
	public static GameObject smcAnimationGroups;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void PlayAnimationGroups(bool rewrite){
		if(groupsTransforms==null){
			Debug.Log ("Can't play animation groups, animation groups array is empty!");
			return;
		}
		if(groupsTransforms.Length<1){
			Debug.Log ("Can't play animation groups, groups array size<1!");
			return;
		}
		int i;
		if(smcAnimationGroups!=null && rewrite)
			Destroy (smcAnimationGroups);
		if(smcAnimationGroups == null){
			smcAnimationGroups = new GameObject();
			smcAnimationGroups.transform.position = Vector3.zero;
			smcAnimationGroups.name = "SMCAnimationGroups";
		}
		
		Transform curGroup;
		Animation curAnim;
		for(i=0;i<groupsTransforms.Length;i++){
			if(groupsTransforms[i]!=null){
				curGroup = Instantiate (groupsTransforms[i]) as Transform;
				curGroup.parent = smcAnimationGroups.transform;
				curGroup.localPosition = Vector3.zero;
				curGroup.name = groupsTransforms[i].name;
				foreach(Transform groupChild in curGroup){
					curAnim = groupChild.GetComponent<Animation>();
					if(curAnim){
						curAnim.enabled = true;
						curAnim.Play();
					}
				}
			}else Debug.Log ("Can't instantiate group "+i+" transform, because its reference is null!");
		}
	}

	public static void PlayAnimationGroups(bool rewrite,bool[] playGroups,WrapMode wrapMode){
		if(groupsTransforms==null){
			Debug.Log ("Can't play animation groups, animation groups array is empty!");
			return;
		}
		if(groupsTransforms.Length<1){
			Debug.Log ("Can't play animation groups, groups array size<1!");
			return;
		}
		int i;
		if(smcAnimationGroups!=null && rewrite)
			Destroy (smcAnimationGroups);
		if(smcAnimationGroups == null){
			smcAnimationGroups = new GameObject();
			smcAnimationGroups.transform.position = Vector3.zero;
			smcAnimationGroups.name = "SMCAnimationGroups";
		}
		
		Transform curGroup;
		Animation curAnim;

		bool playGroupsValid = true;
		if(playGroups == null)
			playGroupsValid = false;
		if(playGroupsValid)
			if(playGroups.Length != groupsTransforms.Length)
				playGroupsValid = false;
		for(i=0;i<groupsTransforms.Length;i++){
			if(groupsTransforms[i]!=null){
				if(playGroupsValid) 
					if(!playGroups[i])
					continue;
				curGroup = Instantiate (groupsTransforms[i]) as Transform;
				curGroup.parent = smcAnimationGroups.transform;
				curGroup.localPosition = Vector3.zero;
				curGroup.name = groupsTransforms[i].name;
				foreach(Transform groupChild in curGroup){
					curAnim = groupChild.GetComponent<Animation>();
					if(curAnim){
						curAnim.enabled = true;
						curAnim.clip.wrapMode = wrapMode;
						curAnim.Play();
					}
				}
			}else Debug.Log ("Can't instantiate group "+i+" transform, because its reference is null!");
		}
	}
}
