using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {
	public Transform target;
	public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = target.position+ new Vector3(0f,0.5f,0f);
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0f, speed*Time.deltaTime, 0f));
	}
}
