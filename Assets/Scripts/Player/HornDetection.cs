using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornDetection : MonoBehaviour {
	public Transform horn;
	bool didActivate;
	public LevelManager levelManager;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool OnClaw(PlayerController.Claw side)
	{
        //If a sea weed is detected when the function is called, it is set as a children and placed on a spot
		if (horn != null && !didActivate)
        {
			didActivate = true;
			StartCoroutine (levelManager.HornCall ());
            return true;
        }
        else
            return false;
	}

	void OnTriggerEnter(Collider other)
	{
		//Keep detected seaweed
		if (other.transform.tag == "Horn" && !PlayerController.inFlow) 
		{
			horn = other.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		//Keep detected exit point
		if (other.transform.tag == "Horn") 
		{
			horn = null;
		}

	}
}
