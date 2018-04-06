using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSeaWeed : MonoBehaviour {
	public LayerMask layer;
	public bool quarter;
	RaycastHit hit;
	// Use this for initialization
	void Start () 
	{
		Ray ray = new Ray (transform.position, -transform.up);
			
		if (Physics.Raycast (ray, out hit, 1f, layer)) 
		{
			print ("hit");
			transform.position = hit.point;
			transform.rotation = GetRotationFromNormal(hit.normal, transform.forward);
		}

		transform.localScale = new Vector3 (Random.Range (1.8f, 2f), Random.Range (1.7f, 2f), Random.Range (1.8f, 2f));

		if (quarter) 
		{
			transform.rotation = Quaternion.Euler(new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Quaternion GetRotationFromNormal(Vector3 normal, Vector3 forward)
	{
		//cross product to get the right forward 
		Vector3 left = Vector3.Cross (forward,normal);
		Vector3 newForward = Vector3.Cross (normal,left);

		//new rotation thanks to normal and forward
		Quaternion newRotation = Quaternion.LookRotation (newForward, normal);

		return newRotation;
	}

}
