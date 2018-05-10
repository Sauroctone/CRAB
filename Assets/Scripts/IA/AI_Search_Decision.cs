using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Search")]
public class AI_Search_Decision : AI_Decision {
	public LayerMask layer;
	public override bool Decide(StateController controller)
	{
		bool noEnemyInSight = Search(controller);
		return noEnemyInSight;
	}

	private bool Search(StateController controller)
	{
		controller.eyeRotator.localRotation = Quaternion.Slerp(controller.eyeRotator.localRotation, Quaternion.Euler (0, Mathf.Sin (controller.stateTimeElapsed*controller.stats.searchRotationSpeed) * controller.stats.searchAngle, 0), 0.5f);
		return controller.CheckIfCountDownElapsed (controller.stats.searchTime);
	}
}
