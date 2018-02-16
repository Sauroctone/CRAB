using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowMovement : MonoBehaviour {
	public FlowManager currentFlow;
	bool inFlow;

	public Transform[] waypoints; 
	float percentPerSecond = 0.01f;
	float currentPathPercent = 0.0f;

	Vector3 previousPosition;
	Vector3 direction;

	void Start()
	{
		previousPosition = transform.position;
	}

	void FixedUpdate()
	{
		if (currentFlow != null) {
			if (!inFlow) 
			{
				inFlow = true;
			}

			if (currentPathPercent <= 1f) {
				currentPathPercent += percentPerSecond * Time.deltaTime;
				iTween.PutOnPath (gameObject, waypoints, currentPathPercent);
				direction = transform.position - previousPosition;
				previousPosition = transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), 0.01f);
			} else if (currentFlow.loops && currentPathPercent >= 1) {
				currentPathPercent = currentPathPercent-1f;
			}
		}
	}

	void OnDrawGizmos()
	{
		iTween.DrawPath (waypoints);
	}
		
	public void InitFlow(FlowManager detectedFlow)
	{
		currentFlow = detectedFlow;
		percentPerSecond = currentFlow.speed;
		waypoints = currentFlow.waypoints;
	}
}
