﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {
	public AI_State currentState;
	public Transform eyes;
	public Transform mouth;
	public Transform eyeRotator;
	public AI_State remainState;
	public AI_Stats stats;
	public List<Transform> wayPointList;
	public LevelManager levelManager;
	public Animator animator;
	public SoundManager_Enemy sM;

	[HideInInspector] public PathFollower follower;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public Vector3 attackTargetPosition;
	[HideInInspector] public Quaternion originRotation;
	[HideInInspector] public Vector3 lastSeenPosition;
	[HideInInspector] public float stateTimeElapsed;
	[HideInInspector] public float pathTimer;
	[HideInInspector] public List<Collider> seenObjects;

	public Coroutine currentMovement;
	public bool aiActive;
	// Use this for initialization
	void Start () {
		SetupIA ();
		//aiActive = false;
	}

	public void SetupIA()
	{
		follower = GetComponent<PathFollower> ();
		originRotation = eyeRotator.rotation;
		aiActive = true;
	}

	void Update()
	{
		if (!aiActive)
			return;
		currentState.UpdateState (this);
		if (pathTimer > 0) 
		{
			pathTimer -= Time.deltaTime;
		}
			
	}

	public void TransitionToState(AI_State nextState)
	{
		if (nextState != remainState) 
		{
			currentState = nextState;
			OnExitState ();
			ResetPath ();
		}
	}

	public bool CheckIfCountDownElapsed(float duration)
	{
		stateTimeElapsed += Time.deltaTime;
		return (stateTimeElapsed >= duration);
	}

	private void OnExitState()
	{
		stateTimeElapsed = 0;
	}

	private void ResetPath()
	{
		if (follower.currentMovement!=null)
			follower.StopCoroutine (follower.currentMovement);
		
		follower.ResetCoroutine ();
	}

	void OnDrawGizmos()
	{
		if (currentState != null && eyes != null) 
		{
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (eyes.position, stats.radius);
		}
	}
}

