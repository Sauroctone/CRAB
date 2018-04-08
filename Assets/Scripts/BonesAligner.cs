using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesAligner : MonoBehaviour {
	FlowInstance flow;
	public Transform[] waypoints;
	public List<Transform> bones = new List<Transform>();
	// Use this for initialization
	void Start () 
	{
		flow = GetComponentInParent <FlowInstance> ();
		waypoints = flow.waypoints;
		Transform[] children = GetComponentsInChildren<Transform> ();

		foreach (Transform child in children) 
		{
			if (child.GetComponent<MeshFilter> () == null)
				bones.Add (child);
		}

		float interval = 100f / bones.Count;

		print (interval);

		for (int i = 0; i < bones.Count; i++) 
		{
			bones [i].position = iTween.PointOnPath (waypoints, i * interval/100);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
