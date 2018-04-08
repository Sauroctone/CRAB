using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowInstance : MonoBehaviour {
	public Transform[] waypoints;
	public bool doesLoop;
	public float speed;
	public float radius;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		iTween.DrawPath (waypoints);
	}
}
