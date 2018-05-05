﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {
	public AI_State currentState;
	public Transform eyes;
	public Transform eyeRotator;
	public AI_State remainState;
	public AI_Stats stats;
	public List<Transform> wayPointList;

	[HideInInspector] public PathFollower follower;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public Vector3 lastSeenPosition;
	[HideInInspector] public float stateTimeElapsed;
	public Coroutine currentMovement;
	bool aiActive;
	// Use this for initialization
	void Start () {
		SetupIA ();
	}

	public void SetupIA()
	{
		follower = GetComponent<PathFollower> ();
		aiActive = true;
	}

	void Update()
	{
		if (!aiActive)
			return;
		currentState.UpdateState (this);
	}

	public void TransitionToState(AI_State nextState)
	{
		print (nextState);
		if (nextState != remainState) 
		{
			currentState = nextState;
			OnExitState ();
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

	void OnDrawGizmos()
	{
		if (currentState != null && eyes != null) 
		{
			Gizmos.color = currentState.sceneGizmoColor;
			Gizmos.DrawWireSphere (eyes.position, stats.radius);
		}
	}
}

