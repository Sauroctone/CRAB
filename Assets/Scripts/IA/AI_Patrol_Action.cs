using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class AI_Patrol_Action : AI_Action
{
	public override void Act(StateController controller)
	{
		Patrol (controller);
	}

	private void Patrol(StateController controller)
	{
		controller.follower.InitMovement (controller.transform.position, controller.wayPointList [controller.nextWayPoint].position, controller.stats.speed);
		/*controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
		controller.navMeshAgent.Resume ();*/

		if ((controller.transform.position - controller.wayPointList [controller.nextWayPoint].position).magnitude <= controller.stats.stopDistance) 
		{
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
		}
	}
}