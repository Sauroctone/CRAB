using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Detector : MonoBehaviour {
	public StateController controller;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		controller.seenObjects.Add (other);
	}

	void OnTriggerExit(Collider other)
	{
		controller.seenObjects.Remove (other);
	}
}
