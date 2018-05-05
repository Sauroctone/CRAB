using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class AI_Look_Decision : AI_Decision {
	public LayerMask layer;
	public override bool Decide(StateController controller)
	{
		bool targetVisible = Look(controller);
		return targetVisible;
	}

	private bool Look(StateController controller)
	{
		Vector3 eyePosition = controller.eyes.position;
		Collider[] seenObjects = Physics.OverlapSphere(eyePosition, controller.stats.radius);
		//Debug.Log (seenObjects.Length);

		foreach (Collider c in seenObjects) 
		{
			if (c.tag == "Player") 
			{
				RaycastHit hit;
				if (Physics.SphereCast (eyePosition, 0.5f, c.transform.position - eyePosition,  out hit, controller.stats.lookRange, layer)
					&& hit.collider.CompareTag ("Player") && (PlayerController.isVisible || controller.chaseTarget == hit.transform)) 
				{
					controller.chaseTarget = hit.transform;
					return true;
				} 
				else 
				{
					if (controller.chaseTarget != null)
					{
						controller.lastSeenPosition = controller.chaseTarget.position;
						controller.chaseTarget = null;
					}
					return false;
				}
			} 
		}

		if (controller.chaseTarget != null) 
		{
			controller.lastSeenPosition = controller.chaseTarget.position;
			controller.chaseTarget = null;
		}
		return false;
	}
}
