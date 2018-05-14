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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && !playing && timelineID < timelines.Length) 
		{
			StartCoroutine ("PlayTimeline");
		}
	}

	IEnumerator PlayTimeline()
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

	}
}
