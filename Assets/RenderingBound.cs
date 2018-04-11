using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingBound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh m = GetComponent<MeshFilter>().mesh;
		m.bounds = new Bounds(Vector3.zero, Vector3.one * 2000);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
