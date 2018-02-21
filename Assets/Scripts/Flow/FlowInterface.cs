using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowInterface : MonoBehaviour {
	[Header("References")]

	FlowMovement flowMovement;
	WaterController waterControl;

	[Header("Detectors")]

	public GameObject flowDetector;
	public GameObject exitDetector;

	[Header("Detected")]

	public List<Transform> detectedWaypoints;
	public Transform detectedExitPoint;
	FlowInstance currentFlow;

	[Header("Transition")]

	public AnimationCurve joiningCurve;

	void Start()
	{
		flowMovement = GetComponentInParent<FlowMovement> ();
		waterControl = GetComponent<WaterController> ();
	}

	/*void Update()
	{
		if (checking && detectedWaypoints.Count > 0) 
		{
			CheckClosestWaypoint ();
		}
	}*/

	void OnTriggerEnter(Collider other)
	{
		//Adds detected waypoint to the list
		if (other.transform.tag == "Waypoint") 
		{
			detectedWaypoints.Add (other.transform);
		}

		//Keep detected exit point
		if (other.transform.tag == "ExitPoint" && PlayerController.inFlow) 
		{
			detectedExitPoint = other.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		//Remove waypoint from the list when to far
		if (other.transform.tag == "Waypoint") 
		{
			detectedWaypoints.Remove (other.transform);
		}

		//Keep detected exit point
		if (other.transform.tag == "ExitPoint" && other.transform == detectedExitPoint) 
		{
			detectedExitPoint = null;
		}

	}

	Transform CheckClosestWaypoint()
	{
		if (detectedWaypoints.Count > 0) 
		{
			float dist = Vector3.Distance (detectedWaypoints [0].transform.position, transform.position);
			Transform closestWP = detectedWaypoints [0];

			//Checks each waypoint, keeps the closest
			for (int i = 0; i < detectedWaypoints.Count; i++) {
				if (dist > Vector3.Distance (detectedWaypoints [i].transform.position, transform.position)) {
					dist = Vector3.Distance (detectedWaypoints [i].transform.position, transform.position);
					closestWP = detectedWaypoints [i];
				}
			}
			return closestWP;
		} 
		else
			return null;
	}

	float CheckPathPercentage(FlowInstance detectedFlow)
	{
		float minDistance = float.PositiveInfinity;
		float minPercent = 0;

		//Check points on the path until closest from the player
		for (float t = 0; t <= 1; t += 0.02f) 
		{
			float dist = Vector3.Distance(transform.position, iTween.PointOnPath(detectedFlow.waypoints, t));
			if (dist < minDistance) {
				minDistance = dist;
				minPercent = t;
			} 
		}
		return minPercent;
	}

	public IEnumerator JoinTheFlow()
	{
		if (CheckClosestWaypoint() != null) 
		{
			//Get the detected flow & position on the path
			FlowInstance targetFlow = CheckClosestWaypoint().GetComponentInParent<FlowInstance> ();
			float targetPercentage = CheckPathPercentage (targetFlow);

			//Init for lerp
			Vector3 originPosition = transform.position;
			float i = 0;

			//Keep up for flow movement
			Vector3 upDirection = transform.up;

			//Lerp towards target point on the path
			while (i < 1) 
			{
				i += 0.01f;
				transform.position = Vector3.Lerp(originPosition, iTween.PointOnPath(targetFlow.waypoints, targetPercentage), joiningCurve.Evaluate(i));
				yield return null;
			}

			//Create flow parent
			GameObject flowParent = new GameObject("FlowParent");
			flowParent.transform.position = transform.position;
			flowParent.transform.rotation = transform.rotation;
			flowMovement = flowParent.AddComponent<FlowMovement> ();
			transform.parent = flowParent.transform;

			//Initiate flow movement and informations for water control
			flowMovement.InitFlow(targetFlow, targetPercentage, upDirection);
			waterControl.InitFlow (targetFlow);
			currentFlow = targetFlow;

			//Switch to inFlow mode
			flowDetector.SetActive (false);
			exitDetector.SetActive (true);
			PlayerController.inFlow = true;
		}
	}

	public IEnumerator ExitPath() //WIP
	{
		yield return null;
		//Destroy flow parent
		transform.parent = null;
		Destroy (flowMovement.gameObject);
		flowMovement = null;

		//Reset waterControl

		//Switch to not in flow mode
		flowDetector.SetActive (true);
		exitDetector.SetActive (false);
		currentFlow = null;
		PlayerController.inFlow = false;
	}

	public Transform GetExitPoint()
	{
		return detectedExitPoint;
	}
}
