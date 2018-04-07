using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public Transform target;
	public PF_Pathfinding pathfinding;
	public float speed;
	Vector3[] path;
	int targetIndex;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			path = (pathfinding.FindPath (transform.position, target.position));
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path [0];

		while (true) 
		{
			if (transform.position == currentWaypoint) 
			{
				targetIndex++;
				if (targetIndex >= path.Length) 
				{
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}

			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed);
			yield return null;
		}
	}
}
