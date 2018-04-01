using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWeedManager : MonoBehaviour {
	public Transform[] slots;
	List<Transform> seaWeeds = new List<Transform>();
	public List<Transform> detectedSeaWeeds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClaw(PlayerController.Claw side)
	{
		if (detectedSeaWeeds.Count != 0 && seaWeeds.Count < 3) 
		{
			detectedSeaWeeds[0].position = slots[seaWeeds.Count].position;
			detectedSeaWeeds[0].parent = slots[seaWeeds.Count];
			detectedSeaWeeds[0].localScale = new Vector3 (0.05f, 0.25f, 0.05f);
			detectedSeaWeeds[0].localRotation = Quaternion.Euler(new Vector3 (0,0,0));
			seaWeeds.Add (detectedSeaWeeds[0]);
			detectedSeaWeeds[0].tag = "Untagged";
			detectedSeaWeeds.RemoveAt(0);
		}
	}

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
