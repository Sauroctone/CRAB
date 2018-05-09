using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggableAI/Stats")]

public class AI_Stats : ScriptableObject {
	[Header("Attack")]
	public float attackRadius;
	public float attackSpeed;
	public float attackCooldown;
	public float attackTime;
	public float offset;
	public AnimationCurve attackCurve;

	[Header("Patrol")]
	public float radius;
	public float lookRange;
	public float patrolRotationSpeed;
	public float patrolAngle;

	[Header("Search")]
	public float searchTime;
	public float searchRotationSpeed;
	public float searchAngle;

	[Header("Chase")]
	public float chaseSpeed;

	public float speed;
	public float stopDistance;
}
