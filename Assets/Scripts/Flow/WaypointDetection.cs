using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDetection : MonoBehaviour {
	public List<Transform> detectedWaypoints;
	FlowMovement flowMovement;

	bool checking;

	void Start()
	{
		checking = true;
		flowMovement = GetComponentInParent<FlowMovement> ();
	}

	void Update()
	{
		if (checking && detectedWaypoints.Count > 0) 
		{
			CheckClosestWaypoint ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Waypoint") 
		{
			detectedWaypoints.Add (other.transform);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Waypoint") 
		{
			detectedWaypoints.Remove (other.transform);
		}
	}

	void CheckClosestWaypoint()
	{
			float dist = Vector3.Distance (detectedWaypoints [0].transform.position, transform.position);
			Transform closestWP = detectedWaypoints [0];

			for (int i = 0; i < detectedWaypoints.Count; i++) {
				if (dist > Vector3.Distance (detectedWaypoints [i].transform.position, transform.position)) {
					dist = Vector3.Distance (detectedWaypoints [i].transform.position, transform.position);
					closestWP = detectedWaypoints [i];
				}
			}

			flowMovement.InitFlow(closestWP.GetComponentInParent<FlowManager> ());
			checking = false;
	}


}
