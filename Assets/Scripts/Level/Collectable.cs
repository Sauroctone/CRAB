using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
	public float amplitude = 0.2f;
	public float floatSpeed = 0.7f;
	public float collectSpeed = 1.5f;
	public float angularSpeed = 200;
	bool collected = false;
	Transform player;

	public bool floating = true;


	// Use this for initialization
	void Start () {
		if (floating)
			StartCoroutine ("Floating");
		
		player = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		print (transform.up);
	}
	
	void OnTriggerEnter(Collider other)//TRIGGER ENTER------------------------------------------------------
	{
		if (other.tag == "Player")//CHECK POUR LES COLLECTABLES---------------------------------
		{
				StartCoroutine ("Collected");
		}//-----------------------------------------------------------------------------------
	}

	//Coroutine : oscillate between position+amplitude && position-amplitude on Y axis
	//Uses sine wave
	IEnumerator Floating()
	{
		float t = 0;
		Vector3 basePos = transform.position;
		while (!collected)
		{
			t += floatSpeed * Time.deltaTime;
			transform.position = basePos + transform.up*Mathf.Sin(t) * amplitude;

			yield return null;
		}
	}

	//Coroutine : rotate around the player while getting closer
	//Then auto-destroy
	IEnumerator Collected()
	{
		collected = true;
		//get distance from player
		transform.parent = player;
		/*
		float distance = Vector2.Distance (new Vector2 (player.position.x, player.position.z), new Vector2 (transform.position.x, transform.position.z));
		//get angle
		float angle = 180 + Vector2.Angle(new Vector2 (player.position.x, player.position.z) - new Vector2 (transform.position.x, transform.position.z), new Vector2 (player.position.x, player.position.z) - new Vector2 (0,0));
		while (distance > 0) 
		{
			//Get closer from player and increment rotation angle
			distance -= collectSpeed * Time.deltaTime;
			angle += angularSpeed * Time.deltaTime;

			//Calculate new coordinate relative to player position
			float rad = angle * Mathf.Deg2Rad;
			Vector3 newPos = new Vector3 (Mathf.Cos (rad), 0, Mathf.Sin (rad)) * distance;

			//Add to player position and update own position
			transform.position = player.position + newPos;*/
		float distance = Vector2.Distance (new Vector2 (0,0), new Vector2 (transform.localPosition.x, transform.localPosition.z));
		//get angle
		float angle = 180 + Vector2.Angle(new Vector2 (transform.localPosition.x, transform.localPosition.z), new Vector2 (0,0));
		while (distance > 0) 
		{
			//Get closer from player and increment rotation angle
			distance -= collectSpeed * Time.deltaTime;
			angle += angularSpeed * Time.deltaTime;

			//Calculate new coordinate relative to player position
			float rad = angle * Mathf.Deg2Rad;
			Vector3 newPos = new Vector3 (Mathf.Cos (rad), 0, Mathf.Sin (rad)) * distance;

			//Add to player position and update own position
			transform.localPosition = newPos;	
			yield return null;
		}
		Destroy (gameObject);

	}

}
