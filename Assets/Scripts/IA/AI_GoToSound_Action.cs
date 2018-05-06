using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GoToSound")]
public class AI_GoToSound_Action : AI_Action
{
	public override void Act(StateController controller)
	{
		GoToSound (controller);
	}

	private void GoToSound(StateController controller)
	{
		if (controller.follower.currentMovement == null)
			controller.follower.InitMovement (controller.transform.position, controller.levelManager.horn.position, controller.stats.chaseSpeed);
		
	}
}