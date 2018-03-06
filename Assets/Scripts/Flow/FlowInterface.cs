using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FlowInterface : MonoBehaviour {
	[Header("References")]

	FlowMovement flowMovement;
	WaterController waterControl;
	PlayerController playerController;

	[Header("Detectors")]

	public GameObject flowDetector;
	public GameObject exitDetector;

	[Header("Detected")]

	public List<Transform> detectedWaypoints;
	public Transform detectedExitPoint;
	FlowInstance currentFlow;

	[Header("Flow Transition")]

	public AnimationCurve joiningCurve;
	public AnimationCurve exitCurve;
	public LayerMask layer;
	RaycastHit hit;
	GameObject theFlowParent;

	[Header("Virtual Cameras")]
	public CinemachineVirtualCamera flowCam;
	public CinemachineFreeLook walkCam;

	void Start()
	{
		flowMovement = GetComponentInParent<FlowMovement> ();
		waterControl = GetComponent<WaterController> ();
		playerController = GetComponent<PlayerController> ();
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
			FlowMode(true);

			//Get the detected flow & position on the path
			FlowInstance targetFlow = CheckClosestWaypoint().GetComponentInParent<FlowInstance> ();
			float targetPercentage = CheckPathPercentage (targetFlow);

			//Init for lerp
			Vector3 originPosition = transform.position;

			//Keep up for flow movement
			Vector3 upDirection = transform.up;

			//Resets local rotation of the mesh inside the player
			StartCoroutine (playerController.ResetMeshRotation ());

			//Lerp towards target point on the path
			for (float i = 0; i < 1; i+=0.01f)
			{
				transform.position = Vector3.Lerp(originPosition, iTween.PointOnPath(targetFlow.waypoints, targetPercentage), joiningCurve.Evaluate(i));
				yield return null;
			}

			//Create flow parent
			GameObject flowParent = new GameObject("FlowParent");
			flowParent.transform.position = transform.position;
			flowParent.transform.rotation = transform.rotation;
			flowMovement = flowParent.AddComponent<FlowMovement> ();
			theFlowParent = flowParent;
			transform.parent = flowParent.transform;

			//Initiate flow movement and informations for water control
			flowMovement.InitFlow(targetFlow, targetPercentage, upDirection);
			waterControl.InitFlow (targetFlow);
			currentFlow = targetFlow;

			//Switch to inFlow mode

			PlayerController.inFlow = true;
		}
	}

	public IEnumerator ExitFlow() 
	{
		//Destroy flow parent
		transform.parent = null;
		Destroy (theFlowParent);
		flowMovement = null;

		//Reset waterControl

		//Init lerp
		Vector3 originPosition = transform.position;
		Quaternion originRotation = transform.rotation;
		Vector3 rayDirection = detectedExitPoint.GetComponent<ExitPoint> ().GetExitTarget (transform) - transform.position;
		Vector3 targetPosition = new Vector3 (0, 0, 0);
		Quaternion targetRotation = new Quaternion(0,0,0,0);

		if (Physics.Raycast (transform.position, rayDirection, out hit, 10f, layer))
		{
			targetPosition = hit.point;
			targetRotation = Quaternion.LookRotation (transform.forward, hit.normal);
		}

		PlayerController.inFlow = false;
		//Lerp & Slerp
		for (float i = 0; i < 1; i+=0.01f)
		{
			//transform.position = Vector3.Lerp(originPosition, targetPosition, exitCurve.Evaluate(i));
			transform.rotation = Quaternion.Slerp (originRotation, targetRotation, exitCurve.Evaluate (i));
			yield return null;
		}

		//Switch to not in flow mode
		FlowMode(false);
		waterControl.ResetFlow ();
		currentFlow = null;

		yield return null;
	}

	public Transform GetExitPoint()
	{
		return detectedExitPoint;
	}

	public void FlowMode(bool value)
	{
		//Detectors
		flowDetector.SetActive (!value);
		exitDetector.SetActive (value);

		//vCams
		flowCam.gameObject.SetActive (value);
		walkCam.gameObject.SetActive (!value);

	}
}
