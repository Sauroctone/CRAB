﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megadaptor : MonoBehaviour {
	MegaShape shape;
	Transform[] flowWaypoints;
	public Mesh mesh;
	public MeshFilter meshF;
	bool once;
	// Use this for initialization
	void Start () 
	{
		meshF.mesh = mesh;
		shape = transform.GetComponent<MegaShape> ();
		flowWaypoints = GetComponentInParent<FlowInstance> ().waypoints;

		for (int i = 0; i < flowWaypoints.Length-1; i++) 
		{
			shape.SetKnotPos (0, i, flowWaypoints [i].localPosition);
		}
		shape.AutoCurve ();
		float stretchValue = shape.CalcLength () / 10f;

		MegaWorldPathDeform deform = GetComponentInChildren<MegaWorldPathDeform> ();
		deform.stretch = stretchValue;

	}
		
	
	// Update is called once per frame
	void Update () {
		if (!once) {
			Mesh m = GetComponentInChildren<MeshFilter> ().mesh;
			m.bounds = new Bounds (Vector3.zero, Vector3.one * 500);
			once = true;
		}
	}

}
