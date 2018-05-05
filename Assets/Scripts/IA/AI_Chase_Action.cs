using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class AI_Chase_Action : AI_Action
{
	public override void Act(StateController controller)
	{
		Patrol (controller);
	}

	private void Patrol(StateController controller)
	{
		controller.follower.InitMovement (controller.transform.position, controller.chaseTarget.position, controller.stats.chaseSpeed);
		/*controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
		controller.navMeshAgent.Resume ();*/
	}
}