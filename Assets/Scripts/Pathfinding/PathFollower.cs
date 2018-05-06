using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

	public Transform target;
	public PF_Pathfinding pathfinding;
	public float speed;
	Vector3[] path;
	int targetIndex;
	Coroutine currentMovement;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			path = (pathfinding.FindPath (transform.position, target.position));
			StartCoroutine ("FollowPath");
		}
	}

	public void InitMovement(Vector3 startPosition, Vector3 targetPosition, float _speed)
	{
		if (currentMovement != null) 
			StopCoroutine (currentMovement);
		
			path = pathfinding.FindPath (startPosition, targetPosition);
			speed = _speed;

		if (path.Length > 0)
			currentMovement = StartCoroutine (FollowPath ());

	}

	void ResetCoroutine()
	{
		targetIndex = 0;
		currentMovement = null;
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path [0];
		print (path.Length);

		while (true) 
		{
			if (transform.position == currentWaypoint) 
			{
				targetIndex++;
				if (targetIndex >= path.Length) 
				{
					ResetCoroutine ();
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}

			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed);
			yield return null;
		}
	}
}
