using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

public class SceneMoCap : MonoBehaviour {
	public static float sampleStep = -1f;
	public static int samples = -1;
	public static MoCapEvent[] events = new MoCapEvent[0];
	public static MoCapGroup[] groups = new MoCapGroup[0];
	public static bool createGroupsPrefabs = true,smcWindow = false,markAsLegacy = true;
	public static string smcPath = "";
	public static System.Type reflection;
	public static bool SMCObjectCreated = false;

	[System.Serializable]
	public class MoCapObject
	{
		public Transform objTransform;
		public bool considerHierarchy = true;
		public AnimationBaker.AnimationFile aClip = new AnimationBaker.AnimationFile();
		public string[] bonesPaths = new string[0];

		public MoCapObject(Transform objectT, bool recordHierarchy){
			objTransform = objectT;
			considerHierarchy = recordHierarchy;
			aClip = new AnimationBaker.AnimationFile();
			bonesPaths = new string[0];
		}
		public MoCapObject(){
			considerHierarchy = true;
			aClip = new AnimationBaker.AnimationFile();
			bonesPaths = new string[0];
		}
	}

	[System.Serializable]
	public class MoCapEvent
	{
		public KeyCode key = KeyCode.F12;
		public float time = -1f;
		public bool active = false;
		//if not OR then AND
		public bool OR = true;
	}

	[System.Serializable]
	public class MoCapGroup
	{
		public string name = "Group";
		public MoCapObject[] objects = new MoCapObject[0];
		public bool isRecording = false;
		public int startEvent = 0;
		public int endEvent = 0;
		public Transform groupTransform;
		public bool createPrefab = false;


		public bool ObjectsAreEmpty(){
			bool result = false;
			int i,j=0;
			if(objects.Length<1)
				result = true;
			if(objects.Length>0){
				for(i=0;i<objects.Length;i++){
					if(objects[i].objTransform == null)
						j++;
				}
				if(objects.Length == j)
					result = true;
			}
			return result;
		}

		public bool AnyObjectHasSamples(){
			bool result = true;
			result = ObjectsAreEmpty();
			if(result)
				return !result;
			int i,j=0;
			for( i=0;i<objects.Length;i++){
				if(objects[i].aClip.IsEmpty())
					j++;
			}
			if(objects.Length == j)
				result = false;
			return result;
		}
	}


	public static void MakePrefabForGroup(int groupId,int objectId,AnimationClip uAnimClip,ref GameObject groupObject){
		//AnimationClip uAnimClip = aClip;
		if(uAnimClip == null){
			Debug.Log ("Current clip is empty!");
			return;
		}
		if(groups[groupId].ObjectsAreEmpty() == false){
			GameObject groupChild;
			Animation anim;
			if(groupObject == null)
			groupObject = new GameObject();
			groupObject.transform.position = Vector3.zero;
			groupObject.name = groups[groupId].name;

			groupChild = Instantiate (groups[groupId].objects[objectId].objTransform.gameObject,Vector3.zero,Quaternion.identity) as GameObject;
			groupChild.transform.parent = groupObject.transform;
			groupChild.name = groups[groupId].objects[objectId].objTransform.name;
			anim = groupChild.GetComponent<Animation>();
			if(anim)
				Destroy(anim);
			groupChild.AddComponent<Animation>();
			anim = groupChild.GetComponent<Animation>();
			if(groupChild.GetComponent<Rigidbody>())
				groupChild.GetComponent<Rigidbody>().isKinematic = true;
			Animator animator = groupChild.GetComponent<Animator>();
			if(animator!=null)
				animator.enabled = false;
			if(anim){
				anim.clip = uAnimClip;
				anim.playAutomatically = false;
			}else Debug.Log ("Can't get Animation component for object '"+groups[groupId].objects[objectId].objTransform.name+"' of group "+
			                 groups[groupId].name);
		}
	}

	public static void ConvertDumpToAnimation(int groupId, int objectId){
		AnimationClip uAnimClip = new AnimationClip();
		reflection = typeof(AnimationClip);
		uAnimClip.name = SceneMoCap.groups[groupId].objects[objectId].objTransform.name;
		uAnimClip.frameRate = SceneMoCap.groups[groupId].objects[objectId].aClip.samples;
		MethodInfo[] clipMethods = reflection.GetMethods ();
		foreach(MethodInfo mI in clipMethods){
			if(mI.Name == "set_legacy"){
				object[] arg = new object[1];
				arg[0] = markAsLegacy;
				mI.Invoke(uAnimClip,arg);
				Debug.Log ("Clip marked as legacy!");
			}
		}
		Debug.Log ("Current sample step for object "+uAnimClip.name+" = "+sampleStep+"; frame rate:"+uAnimClip.frameRate+"; total samples:"+
		           SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples);
		for(int i=0;i<SceneMoCap.groups[groupId].objects[objectId].bonesPaths.Length;i++){
			WriteCurveToClip(ref uAnimClip,groupId,objectId,i);
		}

		string folderPath = smcPath+"/Animations/"+SceneMoCap.groups[groupId].name+"/";
		BINS.CreateFolder(folderPath);
		AssetDatabase.CreateAsset(uAnimClip,folderPath+uAnimClip.name+".anim");
		AssetDatabase.SaveAssets();
		//AssetDatabase.Refresh();
		Debug.Log ("animation file was created:"+uAnimClip.name+".anim");
	}


	public static void ConvertDumpToAnimation(int groupId, int objectId,ref GameObject groupObj){
		AnimationClip uAnimClip = new AnimationClip();
		reflection = typeof(AnimationClip);
		uAnimClip.name = SceneMoCap.groups[groupId].objects[objectId].objTransform.name;
		uAnimClip.frameRate = SceneMoCap.groups[groupId].objects[objectId].aClip.samples;
		MethodInfo[] clipMethods = reflection.GetMethods ();
		foreach(MethodInfo mI in clipMethods){
			if(mI.Name == "set_legacy"){
				object[] arg = new object[1];
				arg[0] = markAsLegacy;
				mI.Invoke(uAnimClip,arg);
				Debug.Log ("Clip marked as legacy!");
			}
		}
		Debug.Log ("Current sample step for object "+uAnimClip.name+" = "+sampleStep+"; frame rate:"+uAnimClip.frameRate+"; total samples:"+
		           SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples);
		for(int i=0;i<SceneMoCap.groups[groupId].objects[objectId].bonesPaths.Length;i++){
			WriteCurveToClip(ref uAnimClip,groupId,objectId,i);
		}
		
		string folderPath = smcPath+"/Animations/"+SceneMoCap.groups[groupId].name+"/";
		BINS.CreateFolder(folderPath);
		AssetDatabase.CreateAsset(uAnimClip,folderPath+uAnimClip.name+".anim");
		MakePrefabForGroup(groupId,objectId,uAnimClip,ref groupObj);
		AssetDatabase.SaveAssets();
		//AssetDatabase.Refresh();
		Debug.Log ("animation file was created:"+uAnimClip.name+".anim");
	}
	

	public static void WriteCurveToClip(ref AnimationClip aC,int groupId,int objectId,int boneIndex){
		AnimationCurve aCurve;
		Keyframe[] keysX = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples],
		keysY = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples],
		keysZ = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples],
		keysW = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		float nextSampleTime = 0f;

		//write position of this bone for all samples
		//===================================================================================
			for(int i=0;i<keysX.Length;i++){
			keysX[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].pos.x);
			keysY[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].pos.y);
			keysZ[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].pos.z);
				nextSampleTime+=sampleStep;
			}
			
				aCurve = new AnimationCurve(keysX);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localPosition.x",aCurve);
			
				aCurve = new AnimationCurve(keysY);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localPosition.y",aCurve);

				aCurve = new AnimationCurve(keysZ);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localPosition.z",aCurve);

		//===================================================================================
		//write rotations
		//===============================================================================================
		keysX = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		keysY = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		keysZ = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		keysW = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		 nextSampleTime = 0f;
		for(int i=0;i<keysX.Length;i++){
			keysX[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].rot.x);
			keysY[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].rot.y);
			keysZ[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].rot.z);
			keysW[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].rot.w);
			nextSampleTime+=sampleStep;
		}
		
		aCurve = new AnimationCurve(keysX);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localRotation.x",aCurve);
		
		aCurve = new AnimationCurve(keysY);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localRotation.y",aCurve);
		
		aCurve = new AnimationCurve(keysZ);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localRotation.z",aCurve);

		aCurve = new AnimationCurve(keysW);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localRotation.w",aCurve);
		//================================================================================================
		//write scales
		//==================================================================================
		keysX = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		keysY = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		keysZ = new Keyframe[SceneMoCap.groups[groupId].objects[objectId].aClip.totalSamples];
		nextSampleTime = 0f;

		for(int i=0;i<keysX.Length;i++){
			keysX[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].scale.x);
			keysY[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].scale.y);
			keysZ[i] = new Keyframe(nextSampleTime,SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[boneIndex].samples[i].scale.z);
			nextSampleTime+=sampleStep;
		}
		
		aCurve = new AnimationCurve(keysX);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localScale.x",aCurve);
		
		aCurve = new AnimationCurve(keysY);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localScale.y",aCurve);
		
		aCurve = new AnimationCurve(keysZ);
		aC.SetCurve(SceneMoCap.groups[groupId].objects[objectId].bonesPaths[boneIndex],typeof(Transform),"localScale.z",aCurve);
	}

	public static int AddEvent(){
		ArrayUtility.Add (ref events,new MoCapEvent());
		return events.Length-1;
	}

	public static int AddEvent(KeyCode key){
		int result = -1;
		ArrayUtility.Add (ref events,new MoCapEvent());
		result = events.Length-1;
		events[result].key = key;
		return result;
	}
	public static int AddEvent(float time){
		int result = -1;
		ArrayUtility.Add (ref events,new MoCapEvent());
		result = events.Length-1;
		events[result].key = KeyCode.None;
		events[result].time = Mathf.Clamp (time,0f,Mathf.Infinity);
		return result;
	}

	public static int AddEvent(KeyCode key,float time, bool OR){
		int result = -1;
		ArrayUtility.Add (ref events,new MoCapEvent());
		result = events.Length-1;
		events[result].key = key;
		events[result].time = Mathf.Clamp (time,0f,Mathf.Infinity);
		events[result].OR = OR;
		return result;
	}

	public static void SetEventProperties(int eventId,KeyCode key){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].key = key;
	}

	public static void SetEventProperties(int eventId,float time){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].time = time;
	}

	public static void SetEventProperties(int eventId,KeyCode key,float time){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].key = key;
		SceneMoCap.events[eventId].time = time;
	}

	public static void SetEventProperties(int eventId,KeyCode key,float time,bool OR){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].key = key;
		SceneMoCap.events[eventId].time = time;
		SceneMoCap.events[eventId].OR = OR;
	}

	public static void ActivateEvent(int eventId){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].active = true;
	}

	public static void DeactivateEvent(int eventId){
		if(eventId<0 || eventId>=SceneMoCap.events.Length){
			Debug.Log ("Event with id "+eventId+" not exist!");
			return;
		}
		SceneMoCap.events[eventId].active = false;
	}

	public static int AddGroup(){
		int result = -1;
		ArrayUtility.Add(ref groups,new MoCapGroup());
		result = groups.Length-1;
		groups[result].name = groups[result].name+result.ToString ();
		return result;
	}

	public static int AddGroup(string name){
		int result = -1;
		ArrayUtility.Add(ref groups,new MoCapGroup());
		result = groups.Length-1;
		groups[result].name = name;
		return result;
	}

	public static int AddGroup(MoCapObject[] objects){
		int result = -1;
		ArrayUtility.Add(ref groups,new MoCapGroup());
		result = groups.Length-1;
		groups[result].name = groups[result].name+result.ToString ();
		groups[result].objects = objects;
		return result;
	}

	public static int AddGroup(string name,MoCapObject[] objects,int startEventId,int endEventId){
		int result = -1;
		ArrayUtility.Add(ref groups,new MoCapGroup());
		result = groups.Length-1;
		groups[result].name = name;
		groups[result].objects = objects;
		if(events.Length>0){
		groups[result].startEvent = Mathf.Clamp(startEventId,0,events.Length-1);
		groups[result].endEvent = Mathf.Clamp (endEventId,0,events.Length-1);
		}else{
			groups[result].startEvent = 0;
			groups[result].endEvent = 0;
		}
		return result;
	}

	public static int AddGroup(string name,Transform[] objects,int startEventId,int endEventId){
		int result = -1;
		ArrayUtility.Add(ref groups,new MoCapGroup());
		result = groups.Length-1;
		MoCapObject[] mcObjects = new MoCapObject[0];
		if(objects !=null){
			if(objects.Length>0){
				for(int i=0;i<objects.Length;i++){
					if(objects[i]!=null)
						ArrayUtility.Add (ref mcObjects,new MoCapObject(objects[i],true));
				}
			}
		}
		groups[result].name = name;
		if(mcObjects.Length>0)
		groups[result].objects = mcObjects;
		else Debug.Log ("All passed objects are empty!");
		if(events.Length>0){
			groups[result].startEvent = Mathf.Clamp(startEventId,0,events.Length-1);
			groups[result].endEvent = Mathf.Clamp (endEventId,0,events.Length-1);
		}else{
			groups[result].startEvent = 0;
			groups[result].endEvent = 0;
		}
		return result;
	}

	public static void SetGroupProperties(int groupId,string name,int startEventId,int endEventId){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		groups[groupId].name = name;
		groups[groupId].startEvent = startEventId;
		groups[groupId].endEvent = endEventId;
	}

	public static void SetGroupProperties(int groupId,string name,Transform[] objects,int startEventId,int endEventId){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		MoCapObject[] mcObjects = new MoCapObject[0];
		if(objects !=null){
			if(objects.Length>0){
				for(int i=0;i<objects.Length;i++){
					if(objects[i]!=null)
						ArrayUtility.Add (ref mcObjects,new MoCapObject(objects[i],true));
				}
			}
		}

		groups[groupId].name = name;
		if(mcObjects.Length>0)
		groups[groupId].objects = mcObjects;
		else Debug.Log ("All passed objects are empty!");
		groups[groupId].startEvent = startEventId;
		groups[groupId].endEvent = endEventId;
	}

	public static void SetGroupProperties(int groupId,string name,Transform[] objects,int startEventId,int endEventId, bool addObjects){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		MoCapObject[] mcObjects = new MoCapObject[0];
		if(objects !=null){
			if(objects.Length>0){
				for(int i=0;i<objects.Length;i++){
					if(objects[i]!=null)
						ArrayUtility.Add (ref mcObjects,new MoCapObject(objects[i],true));
				}
			}
		}
		
		groups[groupId].name = name;
		if(mcObjects.Length>0){
			if(!addObjects){
				groups[groupId].objects = mcObjects;
			}else{
				for(int i=0;i<mcObjects.Length;i++){
					ArrayUtility.Add (ref groups[groupId].objects,mcObjects[i]);
				}
			}
		}else Debug.Log ("All passed objects are empty!");
		groups[groupId].startEvent = startEventId;
		groups[groupId].endEvent = endEventId;
	}

	public static void SetGroupProperties(int groupId,Transform[] objects, bool addObjects){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		MoCapObject[] mcObjects = new MoCapObject[0];
		if(objects !=null){
			if(objects.Length>0){
				for(int i=0;i<objects.Length;i++){
					if(objects[i]!=null)
						ArrayUtility.Add (ref mcObjects,new MoCapObject(objects[i],true));
				}
			}
		}

		if(mcObjects.Length>0){
			if(!addObjects){
				groups[groupId].objects = mcObjects;
			}else{
				for(int i=0;i<mcObjects.Length;i++){
					ArrayUtility.Add (ref groups[groupId].objects,mcObjects[i]);
				}
			}
		}else Debug.Log ("All passed objects are empty!");
	}

	public static void SetGroupProperties(int groupId,Transform[] objects,int startEventId,int endEventId){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		MoCapObject[] mcObjects = new MoCapObject[0];
		if(objects !=null){
			if(objects.Length>0){
				for(int i=0;i<objects.Length;i++){
					if(objects[i]!=null)
						ArrayUtility.Add (ref mcObjects,new MoCapObject(objects[i],true));
				}
			}
		}

		if(mcObjects.Length>0)
			groups[groupId].objects = mcObjects;
		else Debug.Log ("All passed objects are empty!");
		groups[groupId].startEvent = startEventId;
		groups[groupId].endEvent = endEventId;
	}

	public static void SetGroupProperties(int groupId,int startEventId,int endEventId){
		if(groups == null){
			Debug.Log ("Groups array is empty, can't change any properties!");
			return;
		}
		if(groups.Length<1){
			Debug.Log ("Groups length <1, can't change any properties!");
			return;
		}
		if(groupId<0 || groupId>=groups.Length){
			Debug.Log ("Group with ID "+groupId+" not exist!");
			return;
		}
		groups[groupId].startEvent = startEventId;
		groups[groupId].endEvent = endEventId;
	}

}
