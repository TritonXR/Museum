using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class SMCManager : MonoBehaviour {
	int i,j,curBoneId = 0;
	bool keysEvent,timeEvent;
	float passedTime = 0f,nextKeyframeTime=0f;

	// Use this for initialization
	void Start () {
		gameObject.hideFlags = HideFlags.NotEditable;
		if(EditorApplication.isPlaying ){
			if(SceneMoCap.SMCObjectCreated)
				Destroy (transform.gameObject);
			else SceneMoCap.SMCObjectCreated = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(EditorApplication.isPlaying == false){
			if(!SceneMoCap.smcWindow){
				DestroyImmediate(gameObject);
				Debug.Log ("Destroyed from edit mode");
			}
			return;
		}
		passedTime+=Time.unscaledDeltaTime;
		UpdateEvents ();
		if(passedTime>=nextKeyframeTime){
			nextKeyframeTime=passedTime+SceneMoCap.sampleStep*0.5f;
			MakeSamplesForGroups();
		}
	}

	void UpdateEvents(){
		for(i=0;i<SceneMoCap.events.Length;i++){
			if(SceneMoCap.events[i].time>=0 || SceneMoCap.events[i].key != KeyCode.None){
				if(SceneMoCap.events[i].time>=0 && SceneMoCap.events[i].key != KeyCode.None && SceneMoCap.events[i].OR){
					if(passedTime>=SceneMoCap.events[i].time || Input.GetKeyDown(SceneMoCap.events[i].key)){
						SceneMoCap.events[i].active = true;
						SceneMoCap.events[i].OR = false;
					}else SceneMoCap.events[i].active = false;
				}else if(SceneMoCap.events[i].time>=0 && SceneMoCap.events[i].key != KeyCode.None && !SceneMoCap.events[i].OR){
					if(passedTime>=SceneMoCap.events[i].time && Input.GetKeyDown(SceneMoCap.events[i].key)){
						SceneMoCap.events[i].active = true;
					}else SceneMoCap.events[i].active = false;
				}else{
					if(passedTime>=SceneMoCap.events[i].time && SceneMoCap.events[i].time>=0){
						SceneMoCap.events[i].active = true;
						SceneMoCap.events[i].time =-1f;
					}else if(Input.GetKeyDown(SceneMoCap.events[i].key) && SceneMoCap.events[i].key!=KeyCode.None ){
						SceneMoCap.events[i].active = true;
						//Debug.Log ("Key was pressed:"+SceneMoCap.events[i].key.ToString()+"; time:"+Time.time);
					}else{
						SceneMoCap.events[i].active = false;
					}
				}
			}
			if(SceneMoCap.events[i].active){
				Debug.Log ("event "+i+" is active!");
			for(j=0;j<SceneMoCap.groups.Length;j++){
					if(SceneMoCap.groups[j].ObjectsAreEmpty() == false){
						Debug.Log ("group "+j+" start event id:"+SceneMoCap.groups[j].startEvent);
						if(SceneMoCap.groups[j].startEvent == i && !SceneMoCap.groups[j].isRecording){
							SceneMoCap.groups[j].isRecording = true;
							SceneMoCap.groups[j].createPrefab = true;
							Debug.Log ("recording for group "+j+" enabled! Time:"+passedTime);
						}else if(SceneMoCap.groups[j].endEvent == i && SceneMoCap.groups[j].isRecording){
							SceneMoCap.groups[j].isRecording = false;
							//ConvertDumpFiles(j);
							Debug.Log ("recording for group "+j+" disabled! Time:"+passedTime);
						}
					}
				
			}
				if(SceneMoCap.events[i].time<0 && SceneMoCap.events[i].key == KeyCode.None)
					SceneMoCap.events[i].active = false;
			}

		}
		for(j=0;j<SceneMoCap.groups.Length;j++){
			if(!SceneMoCap.groups[j].isRecording && SceneMoCap.groups[j].createPrefab){
				//Debug.Log ("stage1");
				ConvertDumpFiles(j);
				SceneMoCap.groups[j].createPrefab = false;

			}
		}

	}
	

	void EnableDisableRecordingForAllGroups(bool recording){
		for(int a=0;a<SceneMoCap.groups.Length;a++){
			SceneMoCap.groups[a].isRecording = recording;
		}
	}


	void ReadSkeletonHierarchy(Transform h, int groupId,int objectId,bool debug){
		if(!h){
			Debug.Log ("Empty bone detected!");
			return;
		}
		string bonePath = "";
			bonePath = AnimationUtility.CalculateTransformPath(h,SceneMoCap.groups[groupId].objects[objectId].objTransform);
		ArrayUtility.Add(ref SceneMoCap.groups[groupId].objects[objectId].bonesPaths,bonePath);
		if(debug)
			Debug.Log ("hierarchy object "+h.name+" path:"+bonePath);
		foreach(Transform bone in h){
			ReadSkeletonHierarchy(bone,groupId,objectId,debug);
		}
	}

	void WriteHierarchySamples(Transform h,int groupId,int objectId,bool writeHierarchy){
		if(!h){
			Debug.Log ("Empty bone detected!");
			return;
		}

		if(SceneMoCap.groups[groupId].objects[j].aClip.bonesStates == null){
			SceneMoCap.groups[groupId].objects[j].aClip.bonesStates = new AnimationBaker.BoneState[0];
			ArrayUtility.Add (ref SceneMoCap.groups[groupId].objects[j].aClip.bonesStates,new AnimationBaker.BoneState());
		}else if(SceneMoCap.groups[groupId].objects[j].aClip.bonesStates.Length<(curBoneId+1))
			ArrayUtility.Add (ref SceneMoCap.groups[groupId].objects[j].aClip.bonesStates,new AnimationBaker.BoneState());

		SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[curBoneId].AddSample(new AnimationBaker.BoneStateSample(h));
		//Debug.Log ("sample "+SceneMoCap.groups[groupId].objects[objectId].aClip.bonesStates[curBoneId].samples.Length+" recorded for hierarchy unit "+h.name);
		if(writeHierarchy){
			curBoneId++;
			foreach(Transform bone in h){
				WriteHierarchySamples(bone,groupId,objectId,writeHierarchy);
			}
		}
	}


	

	void MakeSamplesForGroups(){
		if(SceneMoCap.groups.Length<1){
			//Debug.Log ("Groups are empty!");
			return;
		}
		for(i=0;i<SceneMoCap.groups.Length;i++){
			if(SceneMoCap.groups[i].ObjectsAreEmpty() ==false){
				if(SceneMoCap.groups[i].isRecording){
			MakeSamplesForGroupObjects(i);
			//Debug.Log ("Frame recorded for group:"+i+", time:"+passedTime+"; nextFrame time:"+nextKeyframeTime);
				}
			}
		}
	}



	void MakeSamplesForGroupObjects(int groupId){
		for(j=0;j<SceneMoCap.groups[groupId].objects.Length;j++){
			if(SceneMoCap.groups[groupId].objects[j].objTransform!=null){
				if(SceneMoCap.groups[groupId].objects[j].bonesPaths.Length<1){
					if(SceneMoCap.groups[groupId].objects[j].objTransform.childCount>0 && SceneMoCap.groups[groupId].objects[j].considerHierarchy){
						ReadSkeletonHierarchy(SceneMoCap.groups[groupId].objects[j].objTransform,groupId,j,true);
						Debug.Log ("objects in hierarchy: "+SceneMoCap.groups[groupId].objects[j].bonesPaths.Length);
					}else SceneMoCap.groups[groupId].objects[j].bonesPaths = new string[]{""};
				}
				curBoneId = 0;
				WriteHierarchySamples(SceneMoCap.groups[groupId].objects[j].objTransform,groupId,j,SceneMoCap.groups[groupId].objects[j].considerHierarchy);
			}
		}
	}

	void ConvertDumpFiles(int groupId){
		GameObject groupObject = new GameObject();
			if(SceneMoCap.groups[groupId].ObjectsAreEmpty() == false){
				for(int j=0;j<SceneMoCap.groups[groupId].objects.Length;j++){
				if(SceneMoCap.groups[groupId].objects[j].aClip.bonesStates !=null){
					if(SceneMoCap.groups[groupId].objects[j].aClip.bonesStates.Length>0){
						SceneMoCap.groups[groupId].objects[j].aClip.samples = SceneMoCap.samples;
						SceneMoCap.groups[groupId].objects[j].aClip.totalSamples = SceneMoCap.groups[groupId].objects[j].aClip.bonesStates[0].samples.Length; 
						SceneMoCap.groups[groupId].objects[j].aClip.name = SceneMoCap.groups[groupId].objects[j].objTransform.name;
						SceneMoCap.ConvertDumpToAnimation(groupId,j,ref groupObject);
						}
					}
				}
			}


		if(SceneMoCap.createGroupsPrefabs && groupObject!=null){
			if(groupObject.transform.childCount>0){
				string folderPath = SceneMoCap.smcPath+"/GroupPrefabs/";
				BINS.CreateFolder(folderPath);
				GameObject newGroup = PrefabUtility.CreatePrefab(folderPath+groupObject.name+".prefab",groupObject);
				Destroy (groupObject);
			}
		}



	}
}
