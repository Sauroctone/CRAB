using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggableAI/Stats")]

public class AI_Stats : ScriptableObject 
{
	[Header("Attack")]
	public float attackRadius;
	public float attackCooldown;
	public float attackTime;
	public float offset;
	public float attackSpeed;
	public AnimationCurve attackCurve;

	[Header("Chase")]
	public float chaseSpeed;

	[Header("Patrol")]
	public float patrolRotationSpeed;
	public float radius;
	public float lookRange;
	public float patrolAngle;

	[Header("Search")]
	public float searchRotationSpeed;
	public float searchTime;
	public float searchAngle;

	public float speed;
	public float stopDistance;
}
