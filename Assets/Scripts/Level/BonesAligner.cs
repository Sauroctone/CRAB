using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesAligner : MonoBehaviour {
	FlowInstance flow;
	public Transform[] waypoints;
	public List<Transform> bones = new List<Transform>();
	Vector3 trueLookAt;
	// Use this for initialization
	void Start () 
	{
		flow = GetComponentInParent <FlowInstance> ();
		waypoints = flow.waypoints;
		Transform[] children = GetComponentsInChildren<Transform> ();

		foreach (Transform child in children) 
		{
			if (child.GetComponent<SkinnedMeshRenderer> () == null && child.GetComponent<Animator> () == null )
				bones.Add (child);
		}

		float interval = 100f / bones.Count;

		print (interval);

		//transform.LookAt(iTween.PointOnPath(waypoints, 0) - iTween.PointOnPath(waypoints, interval));

		/*for (int j = bones.Count-1 ;j > 0; j--)
		{
			bones[j].parent = transform;
		}*/

		for (int i = 0; i < bones.Count; i++) 
		{
			print ((float)i / bones.Count);
			/*bones[i].position = */iTween.PutOnPath(bones[i].gameObject, waypoints, (float)i / bones.Count);
			Vector3 boneDirection =iTween.PointOnPath(waypoints, ((float)i +1)/ bones.Count) - iTween.PointOnPath(waypoints, (float)i / bones.Count);
		
			bones[i].LookAt (bones[i].position + boneDirection); 
			trueLookAt = -bones [i].up;
			bones[i].LookAt (bones[i].position + trueLookAt); 
			bones [i].localEulerAngles += new Vector3 (0, 0, 90);
		}
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
