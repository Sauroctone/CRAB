using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {
	public AI_State currentState;
	public Transform eyes;
	public AI_State remainState;
	public AI_Stats stats;

	[HideInInspector]public PathFollower follower;
	public List<Transform> wayPointList;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;
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
		if (nextState != remainState) 
		{
			currentState = nextState;
			OnExitState ();
		}
	}

	public bool CheckIfCountDownElapsed(float duration)
	{
		stateTimeElapsed += Time.deltaTime;
		return (stateTimeElapsed == duration);
	}

	private void OnExitState()
	{
		stateTimeElapsed = 0;
	}
}

