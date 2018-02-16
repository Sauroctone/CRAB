using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour {
	float hinput;
	float vinput;

	public float amplitude;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		hinput = Input.GetAxis("Horizontal");
		vinput = Input.GetAxis("Vertical");

		transform.localPosition = new Vector3 (hinput * amplitude, vinput * amplitude, transform.localPosition.z);
	}
}
