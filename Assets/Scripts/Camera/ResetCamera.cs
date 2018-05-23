using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResetCamera : MonoBehaviour {
	CinemachineFreeLook cam;
	//public Transform tempTarget;
	//public Transform originTarget;
	Coroutine co;
	// Use this for initialization
	void Start () {
		cam = GetComponent<CinemachineFreeLook> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("ResetCamera")) {
			if (co == null)
				StartCoroutine (Reset ());
		}
	
	}

	IEnumerator Reset()
	{
		cam.m_RecenterToTargetHeading.m_enabled = true;

		yield return new WaitForSecondsRealtime (cam.m_RecenterToTargetHeading.m_RecenteringTime);

		cam.m_RecenterToTargetHeading.m_enabled = false;

	}
}
