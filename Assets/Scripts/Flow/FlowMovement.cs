using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowMovement : MonoBehaviour {
	[Header("Movement")]

	FlowInstance currentFlow;
	public Transform[] waypoints; 
	float speed = 0.01f;
	float currentPathPercent = 0.0f;

	[Header("Direction Update")]

	Vector3 previousPosition;
	Vector3 forwardDirection;
	Vector3 upDirection;

	void Start()
	{
		//Init for direction update
		previousPosition = transform.position;
	}

	void FixedUpdate()
	{
		if (currentFlow != null && PlayerController.inFlow) 
		{
			if (currentPathPercent <= 1f) 
			{
				//Increment progress on the path & update position thanks to the path
				currentPathPercent += speed; 						
				iTween.PutOnPath (gameObject, waypoints, currentPathPercent); 

				//Update orientation from direction
				forwardDirection = transform.position - previousPosition;				
				previousPosition = transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (forwardDirection, upDirection), 0.01f);
			} 

			//Loop back at the begining of the path if needed
			else if (currentFlow.doesLoop && currentPathPercent >= 1) 			
			{
				currentPathPercent = currentPathPercent-1f;
			}
		}
	}

	void OnDrawGizmos()
	{
		iTween.DrawPath (waypoints);
	}
		
	public void InitFlow(FlowInstance detectedFlow, float percentage, Vector3 upVector)
	{
		//Get values from the current flow
		currentFlow = detectedFlow;
		speed = currentFlow.speed;
		currentPathPercent = percentage;
		waypoints = currentFlow.waypoints;
		upDirection = upVector;
	}
}
