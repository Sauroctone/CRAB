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

		foreach (Collider c in controller.seenObjects) 
		{
			
			if (c.tag == "Player") 
			{
				RaycastHit hit;

				if (Physics.Raycast (controller.mouth.position, c.transform.position - controller.mouth.position,  out hit, controller.stats.lookRange, layer)
					&& hit.collider.CompareTag ("Player") && (PlayerController.isVisible || controller.chaseTarget == hit.transform)) 
				{
					controller.chaseTarget = hit.transform;
					controller.lastSeenPosition = controller.chaseTarget.position;
					return true;
				}
			} 
		}

		if (controller.chaseTarget != null) 
		{
			controller.chaseTarget = null;
		}
		return false;
	}
}
