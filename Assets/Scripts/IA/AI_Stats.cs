using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "PluggableAI/Stats")]

public class AI_Stats : ScriptableObject {
	public float patrolSpeed;
	public float chaseSpeed;
	public float stopDistance;
	public float radius;
	public float lookRange;
}
