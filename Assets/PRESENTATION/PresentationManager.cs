using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PresentationManager : MonoBehaviour {
	int timelineID;

	public TimelineAsset[] timelines;
	public PlayableDirector pDir;
	bool playing;

	[Header("Prototype Transition")]
	public GameObject walkCam;
	public GameObject flowCam;
	public float protoID;
	public StateController AIController;


	// Use this for initialization
	void Start () {
		PlayerController.controlsAble = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && !playing && timelineID < timelines.Length) 
		{
			if (timelineID != protoID)
				StartCoroutine ("PlayTimeline");
		}
	}

	public IEnumerator PlayTimeline()
	{
		playing = true;
		pDir.playableAsset = timelines [timelineID];
		pDir.Play ();

		while (pDir.state == PlayState.Playing) 
		{
			yield return null;
		}

		timelineID++;
		playing = false;

		if (timelineID == protoID) 
		{
			yield return new WaitForSeconds (1f);
			PlayModeON ();
		}
	}

	void PlayModeON ()
	{
		walkCam.SetActive (true);
		PlayerController.controlsAble = true;
		AIController.aiActive = true;
	}

	public void PlayModeOFF ()
	{
		walkCam.SetActive (false);
		flowCam.SetActive (false);
		PlayerController.controlsAble = false;
		AIController.aiActive = false;
	}
}
