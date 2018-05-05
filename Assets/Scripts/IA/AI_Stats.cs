using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggableAI/Stats")]

public class AI_Stats : ScriptableObject {
	[Header("Patrol")]
	public float patrolSpeed;
	public float stopDistance;
	public float patrolAngle;
	public float patrolRotationSpeed;

	[Header("Chase")]
	public float chaseSpeed;

	[Header("Detection")]
	public float radius;
	public float lookRange;

	[Header("Search")]
	public float searchAngle;
	public float searchRotationSpeed;
	public float searchTime;
}
