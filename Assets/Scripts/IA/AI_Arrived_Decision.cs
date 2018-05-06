using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Arrived")]
public class AI_Arrived_Decision : AI_Decision {

	public override bool Decide(StateController controller)
	{
		bool arrived = CheckDistance (controller);
		return arrived;
	}

	bool CheckDistance(StateController controller)
	{
		return (controller.transform.position - controller.levelManager.horn.position).magnitude <= controller.stats.stopDistance;
	}

}
