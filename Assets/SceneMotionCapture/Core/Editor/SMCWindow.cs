using UnityEngine;
using System.Collections;
using UnityEditor;

public class SMCWindow : EditorWindow {
	Rect readInputWindowRect,lastRect;
	//Rect[] groupsRects = new Rect[0];
	Vector2 scrollview,scrollview1,scrollview2;
	int i,j,k,samples = 60;
	SceneMoCap.MoCapGroup[] objectsGroups = new SceneMoCap.MoCapGroup[]{new SceneMoCap.MoCapGroup()};
	SceneMoCap.MoCapEvent[] events = new SceneMoCap.MoCapEvent[]{new SceneMoCap.MoCapEvent()};
	Transform smcObjTr;
	int readKeyWindow = -1;
	string orButtonName = "";
	string smcPath = "Assets/SceneMotionCapture";
	bool redIndicator = false, settings = false;
	int indicatorCounter = 0;
	//===Groups player variables===================
	AnimationGroupInfo groupsInfo = new AnimationGroupInfo();
	bool groupsPlayingOptions = false,playMode = false,groupsInstantiated = false,markAsLegacy = true;
	string playButtonName ="";
	Transform[] selectedObjects;
	Event curEvent;
	WrapMode wrapMode = WrapMode.PingPong;


	[MenuItem("Window/AnimationBaker/SceneMotionCapture")]
	public static void OpenSMC(){
		EditorWindow.GetWindow(typeof(SMCWindow));
		SceneMoCap.smcWindow = true;
	}

	/*
	[MenuItem("Window/CloseSMCWindow")]
	public static void ErrorCloseWindow(){
		EditorWindow.GetWindow(typeof(SMCWindow)).Close();
	}
*/
	[System.Serializable]
	class AnimationGroupInfo
	{
		public bool[] playGroups = new bool[0];
		public string[] paths = new string[0];
		public string[] names = new string[0];
	}

	void OnEnable(){
		readInputWindowRect.x =10;
		readInputWindowRect.y =10;
		SceneMoCap.groups = new SceneMoCap.MoCapGroup[0];
		SceneMoCap.events = new SceneMoCap.MoCapEvent[0];

		GameObject smcObj = GameObject.Find ("SMCObject");
		if(smcObj)
			smcObjTr = smcObj.transform;
		if(smcObjTr){
			DestroyImmediate(smcObjTr.gameObject);
			Debug.Log ("SMC object was destroyed when window enabled");
		}
		if(smcObj == null){
			smcObj = new GameObject();
		smcObj.name = "SMCObject";
		smcObj.AddComponent<SMCManager>();
			Debug.Log ("SMCObject was created from window");
		}
		if(smcObj){
			smcObjTr = smcObj.transform;
			//smcObj.hideFlags = HideFlags.HideInHierarchy;
		}
	}


	void RefreshAnimationGroups(){
		groupsInfo = new AnimationGroupInfo();
		int charIdex;
		groupsInfo.paths = AssetDatabase.FindAssets ("t:prefab",new string[]{smcPath+"/GroupPrefabs"});
		if(groupsInfo.paths!=null){
			if(groupsInfo.paths.Length>0){
				for(int i=0;i<groupsInfo.paths.Length;i++){
				groupsInfo.paths[i] = AssetDatabase.GUIDToAssetPath(groupsInfo.paths[i]);
				ArrayUtility.Add(ref groupsInfo.names,groupsInfo.paths[i]);
				ArrayUtility.Add (ref groupsInfo.playGroups,true);
				charIdex = groupsInfo.names[groupsInfo.names.Length-1].LastIndexOf('/');
				groupsInfo.names[groupsInfo.names.Length-1] = groupsInfo.names[groupsInfo.names.Length-1].Remove (0,charIdex+1);
				charIdex = groupsInfo.names[groupsInfo.names.Length-1].IndexOf('.');
groupsInfo.names[groupsInfo.names.Length-1] = groupsInfo.names[groupsInfo.names.Length-1].Remove (charIdex,groupsInfo.names[groupsInfo.names.Length-1].Length-charIdex);
				Debug.Log ("found asset "+i+", path:"+groupsInfo.paths[i]+", name:"+groupsInfo.names[groupsInfo.names.Length-1]);
				}

			}
		}
	}


	void InstantiateAnimationGroups(){
		SMCPlayer.groupsTransforms = new Transform[groupsInfo.names.Length];
		bool[] playGroups = new bool[0];
		for(int i=0;i<groupsInfo.names.Length;i++){
			SMCPlayer.groupsTransforms[i] = AssetDatabase.LoadAssetAtPath(groupsInfo.paths[i],typeof(Transform)) as Transform;
			ArrayUtility.Add (ref playGroups,groupsInfo.playGroups[i]);
		}
		SMCPlayer.PlayAnimationGroups(true,playGroups,wrapMode);
	}


	void OnGUI () {
		curEvent = Event.current;
		SettingsArea();
		GUILayout.BeginHorizontal();
		MoCapGroupsArea ();
		MoCapEventsArea ();
		GUILayout.EndHorizontal ();
	}

	void Update(){
		if(SceneMoCap.smcWindow == false)
			SceneMoCap.smcWindow = true;

		if(EditorApplication.isPlaying){
			if(string.IsNullOrEmpty(SceneMoCap.smcPath))
				SceneMoCap.smcPath = smcPath;
			if(SceneMoCap.sampleStep<0f)
				SceneMoCap.sampleStep = (float)1/samples;
			if(SceneMoCap.samples<0)
				SceneMoCap.samples = samples;
			if(SceneMoCap.markAsLegacy != markAsLegacy)
				SceneMoCap.markAsLegacy = markAsLegacy;
			if(SceneMoCap.groups.Length == 0){
				SceneMoCap.groups = objectsGroups;
			}
			if(SceneMoCap.events.Length == 0){
				SceneMoCap.events = events;
			}
			if(SceneMoCap.groups.Length>0 && SceneMoCap.events.Length>0){
				objectsGroups = SceneMoCap.groups;
				events  = SceneMoCap.events;
			}
			if(playMode && !groupsInstantiated){
				InstantiateAnimationGroups();
				groupsInstantiated = true;
			}
		}else{ 
			if(SceneMoCap.groups.Length>0 && SceneMoCap.events.Length>0){
			SceneMoCap.groups = new SceneMoCap.MoCapGroup[0];
			SceneMoCap.events = new SceneMoCap.MoCapEvent[0];
			}
			if(playMode && groupsInstantiated){
				playMode = false;
				groupsInstantiated = false;
			}
		}


		//need for indicating recording of group==========
		indicatorCounter++;
		if (indicatorCounter > 60) {
			indicatorCounter=0;
			if(redIndicator)
				redIndicator = false;
			else redIndicator = true;
		}
		Repaint ();
	}

	void ReadKeyWindow(int id){
		GUILayout.Label ("Press any key");
		if(Event.current.isKey){
			events[id].key = Event.current.keyCode;
			Debug.Log ("pressed key: "+events[id].key.ToString());
			readKeyWindow = -1;
		}
		//GUI.DragWindow();
	}

	void SettingsArea(){
		GUILayout.BeginVertical ();
		if(!settings){
			if(GUILayout.Button ("Settings",GUILayout.Width (80)))
				settings = true;
		}else{
			GUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("SMC folder:",EditorStyles.boldLabel,GUILayout.Width (80));
			GUILayout.Label (smcPath);
			if(GUILayout.Button ("Select"))
				smcPath = EditorUtility.OpenFolderPanel("Select root Scene Motion Capture folder",smcPath,"SceneMotionCapture");
			GUILayout.EndHorizontal ();
			SamplesValueArea();
			markAsLegacy = EditorGUILayout.ToggleLeft("Mark clip as legacy",markAsLegacy);
			if(GUILayout.Button ("Close",GUILayout.Width (80)))
				settings = false;
		}
		GUILayout.EndVertical ();

	}


	void SamplesValueArea(){
		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Samples ",EditorStyles.boldLabel,GUILayout.Width (100));
		samples = EditorGUILayout.IntField(samples,GUILayout.Width (60));
		GUILayout.EndHorizontal ();
		samples = Mathf.Clamp (samples,1,10000);
	}

	void GroupsPlayerArea(){
		GUI.backgroundColor = Color.yellow;
		if(GUILayout.Button ("Refresh animation groups"))
			RefreshAnimationGroups();
		if(groupsInfo.names.Length>0){
			if(playMode == false)
				playButtonName = "Play groups";
			else playButtonName = "Stop playing";
			if(GUILayout.Button (playButtonName)){
				if(playMode){
					EditorApplication.isPlaying = false;
					playMode = false;
					groupsInstantiated = false;
				}else{
					EditorApplication.isPlaying = true;
					playMode = true;
				}
			}
		scrollview2 = GUILayout.BeginScrollView(scrollview2,GUILayout.Height(50));
		GUILayout.BeginHorizontal ();
		//GUILayoutOption.
		for(k=0;k<groupsInfo.playGroups.Length;k++){
			groupsInfo.playGroups[k] = EditorGUILayout.ToggleLeft(groupsInfo.names[k],groupsInfo.playGroups[k],GUILayout.MaxWidth(groupsInfo.names[k].Length*10+10));
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (10);
		GUILayout.EndScrollView();
		}
		wrapMode = (WrapMode)EditorGUILayout.EnumPopup("Select wrap mode ",wrapMode) ;
		GUI.backgroundColor = Color.white;
	}

	void MoCapGroupsArea(){
		scrollview = GUILayout.BeginScrollView(scrollview);
		GUILayout.BeginVertical ();
		EditorGUILayout.LabelField("Groups:",EditorStyles.boldLabel);
		//debug
		if(EditorApplication.isPlaying)
			GUILayout.Label ("SceneMoCap.groups.Length = "+SceneMoCap.groups.Length);
		//end debug
		if(GUILayout.Button ("Add group")){
			ArrayUtility.Add (ref objectsGroups,new SceneMoCap.MoCapGroup());
			objectsGroups[objectsGroups.Length-1].name = "Group"+objectsGroups.Length.ToString();
		}
		GUILayout.Box ("",GUILayout.Height (5),GUILayout.ExpandWidth (true));
		//groups player section ==============================================================================
			groupsPlayingOptions = EditorGUILayout.Foldout (groupsPlayingOptions,"Groups playing options");
			if(groupsPlayingOptions)
			GroupsPlayerArea();
		//====================================================================================================
		GUILayout.Box ("",GUILayout.Height (5),GUILayout.ExpandWidth (true));
		if(objectsGroups.Length>0){
			for(i=0;i<objectsGroups.Length;i++){
				if(objectsGroups[i].isRecording == false)
				MoCapGroupField(i);
				else 
				IndicateGroupRecording(i);
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndScrollView();
	}

	void MoCapObjectField(int groupId,int objectId){
		GUILayout.BeginHorizontal ();
		GUILayout.BeginVertical();
		//objectsGroups[groupId].objects[objectId].objTransform = (Transform)EditorGUILayout.ObjectField((objectId+1).ToString (),objectsGroups[groupId].objects[objectId].objTransform,
		//typeof(Transform),true);
		EditorGUILayout.LabelField ((objectId+1).ToString ()+" "+objectsGroups[groupId].objects[objectId].objTransform.name,EditorStyles.boldLabel);
		objectsGroups[groupId].objects[objectId].considerHierarchy = EditorGUILayout.Toggle ("Record whole hierarchy",objectsGroups[groupId].objects[objectId].considerHierarchy);
		GUILayout.EndVertical ();
		if(GUILayout.Button("X",GUILayout.Width (20)))
			ArrayUtility.RemoveAt(ref objectsGroups[groupId].objects,j);
		GUILayout.EndHorizontal ();

	}

	void MoCapGroupField(int id){
		objectsGroups[id].name = EditorGUILayout.TextField("Group name",objectsGroups[id].name);
		//if(GUILayout.Button("Add object",GUILayout.ExpandWidth (false)))
			//ArrayUtility.Add (ref objectsGroups[id].objects,new SceneMoCap.MoCapObject());
		GUI.color = Color.green;
		GUILayout.Box ("Select desired objects in scene and click to this area");
		lastRect = GUILayoutUtility.GetLastRect();
		if(curEvent.type == EventType.MouseDown){
			if(lastRect.Contains(curEvent.mousePosition)){
				curEvent.Use ();
			selectedObjects = Selection.transforms;
				if(selectedObjects!=null){
					if(selectedObjects.Length>0){
						//objectsGroups[id].objects = new SceneMoCap.MoCapObject[selectedObjects.Length];
						for(j=0;j<selectedObjects.Length;j++){
							ArrayUtility.Add(ref objectsGroups[id].objects,new SceneMoCap.MoCapObject());
							objectsGroups[id].objects[objectsGroups[id].objects.Length-1].objTransform = selectedObjects[j];
						}
					}
				}
			}
		}
		if(objectsGroups[id].objects.Length>0){
			EditorGUILayout.LabelField ("Objects:",EditorStyles.boldLabel);
			for(j=0;j<objectsGroups[id].objects.Length;j++){
				if(objectsGroups[id].objects[j].objTransform!=null)
				MoCapObjectField(id,j);
			}

			GUILayout.Space (20);
		}
		GUI.color = Color.white;
		objectsGroups[id].startEvent = EditorGUILayout.IntField("Recording start event id",objectsGroups[id].startEvent);
		objectsGroups[id].endEvent = EditorGUILayout.IntField("Recording stop event id",objectsGroups[id].endEvent);
		if(events.Length>0){
		objectsGroups[id].startEvent = Mathf.Clamp (objectsGroups[id].startEvent,0,events.Length-1);
		objectsGroups[id].endEvent = Mathf.Clamp (objectsGroups[id].endEvent,0,events.Length-1);
		}else{
			objectsGroups[id].startEvent = 0;
			objectsGroups[id].endEvent = 0;
		}
		GUILayout.BeginHorizontal ();
		GUI.color = Color.red;
		GUILayout.Label ("Remove group ");
		if(GUILayout.Button ("X",GUILayout.Width (20))){
			ArrayUtility.RemoveAt(ref objectsGroups,id);
		}
		GUI.color = Color.white;
		GUILayout.EndHorizontal ();
		GUILayout.Space(10);
		GUILayout.Box ("",GUILayout.Height(5),GUILayout.ExpandWidth(true));
	}

	void IndicateGroupRecording(int id){
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		if (redIndicator)
			GUI.color = Color.red;
		else
			GUI.color = Color.black;
		GUILayout.Box ("", GUILayout.Height (10), GUILayout.Width (10));
		GUI.color = Color.white;
		GUILayout.Label ("Group ",GUILayout.Width(40));
		EditorGUILayout.LabelField (objectsGroups [id].name, EditorStyles.boldLabel,GUILayout.Width(objectsGroups[i].name.Length*10));
		GUILayout.Label (" is recording",GUILayout.Width(80));
		GUILayout.EndHorizontal ();
		GUILayout.Label ("Uses events: start = "+objectsGroups [id].startEvent+"; end = "+objectsGroups [id].endEvent);
		GUILayout.EndVertical ();
	}

	void MoCapEventsArea(){
		GUILayout.BeginHorizontal (GUILayout.MinWidth(250));
		GUILayout.Box ("",GUILayout.Width (5),GUILayout.ExpandHeight(true));
		scrollview1 = GUILayout.BeginScrollView(scrollview1);
		GUILayout.BeginVertical ();
		EditorGUILayout.LabelField("Events:",EditorStyles.boldLabel);
		//debug
		if(EditorApplication.isPlaying)
			GUILayout.Label ("SceneMoCap.events.Length = "+SceneMoCap.events.Length);
		//end debug
		if(GUILayout.Button ("Add event")){
			ArrayUtility.Add (ref events,new SceneMoCap.MoCapEvent());
		}
		GUILayout.Space (10);
		GUILayout.Box ("",GUILayout.Height(5),GUILayout.ExpandWidth(true));
		if(events.Length>0)
		for(i=0;i<events.Length;i++){
			MoCapEventField(i);
		}
		GUILayout.EndVertical ();
		GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
	}
	

	void MoCapEventField(int id){
		GUILayout.Label ("Event ID: "+id,GUILayout.Width (80));
		//key event behaviour =====================================
		if(events[id].key == KeyCode.None){
		if(GUILayout.Button ("Add key event",GUILayout.Width(100)))
			events[id].key = KeyCode.F12;
		}else{
			GUILayout.BeginHorizontal();
			GUILayout.Label ("Key: "+events[id].key.ToString(),GUILayout.Width (60));
			if(GUILayout.Button ("Read key",GUILayout.Width(80))){
					readKeyWindow = id;
			}
			lastRect = GUILayoutUtility.GetLastRect();	
				
		if(GUILayout.Button ("X",GUILayout.Width (20)))
			events[id].key = KeyCode.None;
			GUILayout.EndHorizontal();
		}
		//=========================================================
		//OR/AND button behaviour==========================================
		if(events[id].key != KeyCode.None && events[id].time>=0){
			if(events[id].OR)
				orButtonName = "OR";
			else orButtonName = "AND";
			if(GUILayout.Button (orButtonName,GUILayout.Width (40),GUILayout.Height (30))){
				if(events[id].OR)
					events[id].OR = false;
				else events[id].OR = true;
			}
		}
		//=================================================================
		//time event behaviour======================================
		if(events[id].time<0){
			if(GUILayout.Button("Add time event",GUILayout.Width (100)))
				events[id].time = 0f;
		}else{
			GUILayout.BeginHorizontal();
			events[id].time = EditorGUILayout.FloatField("Time",events[id].time);
				events[id].time = Mathf.Clamp (events[id].time,0f,Mathf.Infinity);
			if(GUILayout.Button ("X",GUILayout.Width (20)))
				events[id].time = -1f;
			GUILayout.EndHorizontal();
		}
		if(events[id].key == KeyCode.None && events[id].time<0){
			GUILayout.Box("Can be activated only from script using ID");
		}

		GUILayout.BeginHorizontal ();
		GUILayout.Label("Remove event");
		GUI.color = Color.red;
		if(GUILayout.Button ("X",GUILayout.Width (20)))
			ArrayUtility.RemoveAt (ref events,id);
		GUI.color = Color.white;
		GUILayout.EndHorizontal ();
		if(readKeyWindow == id){
			readInputWindowRect.x = lastRect.x;
			readInputWindowRect.y = lastRect.y;
			//readInputWindowRect = new Rect(lastRect.y,lastRect.x,100f,40f);
			BeginWindows ();
			readInputWindowRect = GUILayout.Window(id,readInputWindowRect,ReadKeyWindow,"Reads key");
			EndWindows ();
			GUI.FocusWindow(id);
		}
		GUILayout.Space (10);
		GUILayout.Box ("",GUILayout.Height(5),GUILayout.ExpandWidth(true));
	}


	void OnDisable(){
		GameObject smcObj = GameObject.Find("SMCObject");
		if(smcObj)
			smcObjTr = smcObj.transform;
		if(smcObjTr){
			DestroyImmediate(smcObjTr.gameObject);
			Debug.Log ("SMC object was destroyed from window");
		}
		SceneMoCap.smcWindow = false;
	}
}
