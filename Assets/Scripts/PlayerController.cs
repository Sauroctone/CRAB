﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement")]

    float hinput;
    float vinput;
    public float speed;
    Vector3 direction;
    Vector3 movement;
	public static bool inFlow = false;
	Vector3 lastDirection;
	public Transform meshObject;

    [Header("Raycast and rotation")]

    public LayerMask layer;
    RaycastHit hit;
    public float rotLerp;
	bool onFloor;


    [Header("References")]

    public Rigidbody rb;

	void Update ()
    {
        //Calculate the direction vector based on inputs

        hinput = Input.GetAxisRaw("Horizontal");
        vinput = Input.GetAxisRaw("Vertical");

        direction = new Vector3(hinput, 0f, vinput).normalized;

       	movement = direction * speed;



    }

    void FixedUpdate()
    {
		
		if (!inFlow) 
		{
			//Set velocity based on movement vector
			rb.velocity = Camera.main.transform.TransformDirection (movement);

			//Get and paply new rotation of the player depending of the surface
			Quaternion newRotation = GetRotationFromNormal (GetNormalAverage ());
			rb.MoveRotation (Quaternion.Slerp(transform.rotation,newRotation,rotLerp));

			//Change mesh orientation with movement
			if (direction.magnitude > 0.2f) 
			{
				meshObject.localRotation = Quaternion.Slerp(meshObject.localRotation, Quaternion.LookRotation (movement, Vector3.up), 0.1f);
				lastDirection = movement;
			} 
			else if (lastDirection != Vector3.zero) //Keeps last orientation if the player is not moving
			{
				meshObject.localRotation = Quaternion.Slerp(meshObject.localRotation, Quaternion.LookRotation (lastDirection, Vector3.up), 0.1f);
			}

			//false "gravity", keeps the player on the surface
			if (!onFloor) 
			{
				rb.velocity += -transform.up;
			}
		}
    }

	//////////////Updates a boolean tracking if the player is touching the floor or not
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Wall" && !onFloor) 
		{
			onFloor = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Wall" && onFloor) 
		{
			onFloor = true;
		}
	}


	//////////////Functions for orienting the player depending of the surface 

	//Get new rotation from the surface normal
	Quaternion GetRotationFromNormal(Vector3 normal)
	{
		//cross product to get the right forward 
		Vector3 left = Vector3.Cross (transform.forward,normal);
		Vector3 newForward = Vector3.Cross (normal,left);

		//new rotation thanks to normal and forward
		Quaternion newRotation = Quaternion.LookRotation (newForward, normal);

		return newRotation;
	}

	//Get the average normal from 4 raycasts
	Vector3 GetNormalAverage()
	{
		//Init rays for raycast and variable for the average
		List<Ray> rays = new List<Ray>();
		Ray fRay = new Ray (meshObject.position + meshObject.forward*0.1f, -transform.up);
		Ray bRay = new Ray (meshObject.position - meshObject.forward*0.1f, -transform.up);
		Ray lRay = new Ray (meshObject.position + Vector3.Cross (meshObject.forward, transform.up)*0.3f, -transform.up);
		Ray rRay = new Ray (meshObject.position - Vector3.Cross (meshObject.forward, transform.up)*0.3f, -transform.up);

		rays.Add (fRay);
		rays.Add (bRay);
		rays.Add (lRay);
		rays.Add (rRay);

		Vector3 averageNormal = new Vector3 (0,0,0);

		//Adding each raycast's normal to the average
		for (int i = 0; i < rays.Count; i++) 
		{
			if (Physics.Raycast (rays[i], out hit, 1f)) 
			{
				Debug.DrawRay (rays[i].origin, rays[i].direction, Color.red);
				averageNormal += hit.normal;
			}
		}
		//Then divide by number of normals added
		averageNormal /= rays.Count;

		Debug.DrawRay (transform.position, averageNormal, Color.green);

		return averageNormal;
	}

	//Resets local rotation of the messh to (0,0,0)
	public IEnumerator ResetMeshRotation()
	{
		Quaternion startRotation = meshObject.localRotation;
		Quaternion resetRotation = Quaternion.EulerAngles (new Vector3 (0, 0, 0));

		for (float i = 0f; i <= 1f; i += Time.deltaTime) 
		{
			meshObject.localRotation = Quaternion.Slerp(startRotation, resetRotation, i);
			yield return null;
		}

		meshObject.localRotation = resetRotation;
	}
}
