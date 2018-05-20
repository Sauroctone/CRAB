using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LevelManager : MonoBehaviour {
	public bool called;
	public Transform horn;
	public GameObject exitFlow;
	public PlayableDirector pDir;
	public TimelineAsset callTimeline;
	public PresentationManager director;

	public IEnumerator HornCall()
	{
		PlayerController.controlsAble = false;
		pDir.playableAsset = callTimeline;
		pDir.Play ();

		while (pDir.state == PlayState.Playing) 
		{
			yield return null;
		}

		PlayerController.controlsAble = true;

		called = true;
		yield return new WaitForSeconds (0.5f);
		called = false;

		//Play sound

		//exitFlow.SetActive (true);
	}

	private IEnumerator EndLevel()
	{
		yield return null;
		director.StartCoroutine (director.PlayTimeline ());
		director.PlayModeOFF ();
		print ("The end");

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			//End level
			StartCoroutine(EndLevel());

		}
	}

}
