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
			controller.follower.InitMovement (controller.transform.position, controller.wayPointList [controller.nextWayPoint].position, controller.stats.patrolSpeed);
		/*controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
		controller.navMeshAgent.Resume ();*/

		if ((controller.transform.position - controller.wayPointList [controller.nextWayPoint].position).magnitude <= controller.stats.stopDistance) 
		{
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
			controller.follower.InitMovement (controller.transform.position, controller.wayPointList [controller.nextWayPoint].position, controller.stats.patrolSpeed);
		}
	}
}