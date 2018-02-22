using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour {
	float radius;
	// Use this for initialization
	void Start () {
		radius = 0.5f;
	}
	
	public Vector3 GetExitTarget(Transform playerTransform)
	{
		float minDistance = Mathf.Infinity;
		Vector3 targetPoint = new Vector3 (0,0,0);

		for (float i = 0; i <= 360; i++) 
		{
			Vector3 testDirection = new Vector3 (Mathf.Sin (i) * radius, 0, Mathf.Cos (i) * radius);
			Vector3 testPoint = transform.TransformDirection (testDirection) + transform.position;
			if (Vector3.Distance (playerTransform.position, testPoint) < minDistance) 
			{
				minDistance = Vector3.Distance (playerTransform.position, testPoint);
				targetPoint = testPoint;
			}
		}
		return targetPoint;
	}
}
