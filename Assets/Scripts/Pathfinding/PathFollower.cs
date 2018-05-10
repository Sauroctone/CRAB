using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

	public Transform target;
	public PF_Pathfinding pathfinding;
	public float speed;
	public StateController controller;
	public AI_State chasingState;
	public LayerMask playerLayer;
	Vector3[] path;
	int targetIndex;
	public Coroutine currentMovement;
	Coroutine currentAttack;

	public void InitMovement(Vector3 startPosition, Vector3 targetPosition, float _speed)
	{
		if (currentMovement != null) 
			StopCoroutine (currentMovement);
		
			path = pathfinding.FindPath (startPosition, targetPosition);
			speed = _speed;

		if (path != null && path.Length > 0)
			currentMovement = StartCoroutine (FollowPath ());
	}

	public void LookAtTarget()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation (controller.chaseTarget.position - transform.position, Vector3.up), 0.1f);
	}

	public void ResetCoroutine()
	{
		targetIndex = 0;
		currentMovement = null;
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
					ResetCoroutine ();
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}

			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed);

			if (controller.currentState == chasingState) {
				LookAtTarget ();
			} else {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (currentWaypoint - transform.position, Vector3.up), 0.01f);
			}
			yield return new WaitForFixedUpdate();
		}
	}

	public void Attack()
	{
		if (currentAttack == null) {
			currentAttack = StartCoroutine (AttackCoroutine ());
			currentMovement = null;
		}
	}

	IEnumerator AttackCoroutine()
	{
		//Init
		Vector3 target = controller.attackTargetPosition;
		Vector3 origin = transform.position;
		Vector3 direction = (target - origin).normalized; 
		target -= direction * controller.stats.offset;

		//Dash in
		for (float i = 0; i < 1; i+= Time.deltaTime*controller.stats.attackSpeed)
		{
			transform.position = Vector3.Lerp(origin, target, controller.stats.attackCurve.Evaluate(i));

			yield return new WaitForFixedUpdate();
		}

		//Check hit
		Collider[] attackable = Physics.OverlapSphere(controller.mouth.position, controller.stats.attackRadius, playerLayer);

		foreach (Collider c in attackable) 
		{
			if (c.tag == "Player") 
			{
				PlayerController playerC = c.GetComponentInParent<PlayerController> ();
				playerC.StartCoroutine (playerC.Die ());
				controller.aiActive = false;
			}
		}

		//Dash out
		origin -= direction * 0.5f;

		for (float i = 1; i > 0; i-= Time.deltaTime*controller.stats.attackSpeed)
		{
			transform.position = Vector3.Lerp(origin, target, controller.stats.attackCurve.Evaluate(i));

			yield return new WaitForFixedUpdate();
		}

		currentAttack = null;
	}
}
