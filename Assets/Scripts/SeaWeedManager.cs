using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedManager : MonoBehaviour {
	public Transform[] slots;
	List<Transform> seaWeeds = new List<Transform>();
	public List<Transform> detectedSeaWeeds;

	Rigidbody rB;

	// Use this for initialization
	void Start () {
		rB = GetComponentInParent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (seaWeeds.Count >= 1 && rB.velocity.magnitude < 0.1f)
			PlayerController.isVisible = false;
		else
			PlayerController.isVisible = true;
	}

	public void OnClaw(PlayerController.Claw side)
	{
		//If a sea weed is detected when the function is called, it is set as a children and placed on a spot
		if (detectedSeaWeeds.Count != 0 && seaWeeds.Count < 3) 
		{
			detectedSeaWeeds[0].position = slots[seaWeeds.Count].position;
			detectedSeaWeeds[0].parent = slots[seaWeeds.Count];
			Vector3 scaleSave = detectedSeaWeeds [0].localScale;
			detectedSeaWeeds[0].localScale = new Vector3 (scaleSave.x/2, scaleSave.y/2, scaleSave.z/2);
			detectedSeaWeeds[0].localRotation = Quaternion.Euler(new Vector3 (0,Random.Range(-135,135),0));
			seaWeeds.Add (detectedSeaWeeds[0]);
			detectedSeaWeeds[0].tag = "Untagged";
			detectedSeaWeeds.RemoveAt(0);
		}
	}

	//Called when the player 
	public void LoseSeaWeeds()
	{
		for(int i = seaWeeds.Count-1; i >= 0; i--) 
		{
			Transform currentSeaWeed = seaWeeds [i];
			seaWeeds.Remove (seaWeeds[i]);
			Destroy (currentSeaWeed.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//Keep detected seaweed
		if (other.transform.tag == "SeaWeed" && !PlayerController.inFlow) 
		{
			detectedSeaWeeds.Add(other.transform);
		}
	}

	void OnTriggerExit(Collider other)
	{
		//Keep detected exit point
		if (other.transform.tag == "SeaWeed" ) 
		{
			detectedSeaWeeds.Remove(other.transform);
		}

	}
}
