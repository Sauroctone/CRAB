using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {
	[Header("Movement")]

	public float swimForce;
	float hOffset;
	float vOffset;
	float hinput;
	float vinput;


	[Header("Flow parameter")]

	public float centerForce;
	float amplitude;

	[Header("Transition")]
	public bool exiting;

	[Header("References")]

	FlowInterface flowInterface; 

	// Use this for initialization
	void Start () {
		flowInterface = GetComponent<FlowInterface> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		//Check if the player let go
		if (Input.GetButtonDown ("Let Go") && !PlayerController.inFlow) 
		{
			StartCoroutine (flowInterface.JoinTheFlow ());
		}

		if ((PlayerController.leftClaw || PlayerController.rightClaw) && flowInterface.GetExitPoint() != null)
		{
			float dirNum = AngleDir (transform.forward, flowInterface.GetExitPoint ().position - transform.position, transform.up);
			if (dirNum  < 0 && PlayerController.leftClaw && !exiting) 
			{
				print ("left");
				StartCoroutine (flowInterface.ExitFlow ());
				exiting = true;
			} 
			else if (dirNum > 0 && PlayerController.rightClaw && !exiting)
			{
				print ("right");
				StartCoroutine (flowInterface.ExitFlow ());
				exiting = true;
			}
		}

		//Get stick input
		hinput = Input.GetAxis ("Horizontal");
		vinput = Input.GetAxis ("Vertical");
	}

	void FixedUpdate () 
	{
		if (PlayerController.inFlow && PlayerController.controlsAble) 
		{
			//Swim movement from the player
			hOffset += swimForce * hinput;
			vOffset += swimForce * vinput;

			//Stay inside the radius
			Vector3 newPos = new Vector3 (hOffset, vOffset, 0);
			Vector3 clampedPos = Vector3.ClampMagnitude (newPos, amplitude);

			//Attract player if not a at the center
			if (clampedPos.magnitude > 0) {
				clampedPos -= Vector3.ClampMagnitude (clampedPos, centerForce); 
			}

			//Update offset
			hOffset = clampedPos.x;
			vOffset = clampedPos.y;

			//Update player position
			transform.localPosition = clampedPos;
		}
	}

	public void InitFlow (FlowInstance currentFlow)
	{
		amplitude = currentFlow.radius;
	}

	public void ResetFlow()
	{
		amplitude = 0f;
		exiting = false;
		hOffset = 0f;
		vOffset = 0f;
	}

	//Returns -1 if target is left and 1 if right (0 if straight forward or backward)
	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
	{
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}

}
