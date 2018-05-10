using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public bool called;
	public Transform horn;
	public GameObject exitFlow;

	public IEnumerator HornCall()
	{
		called = true;
		yield return new WaitForSeconds (0.5f);
		called = false;

		//Play sound

		exitFlow.SetActive (true);
	}

	private IEnumerator EndLevel()
	{
		yield return null;
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
