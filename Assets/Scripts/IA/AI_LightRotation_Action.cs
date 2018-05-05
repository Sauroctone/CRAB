using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Rotate")]
public class AI_LightRotation_Action : AI_Action
{
	public override void Act(StateController controller)
	{
		Rotate (controller);
	}

	private void Rotate(StateController controller)
	{
		controller.CheckIfCountDownElapsed (Mathf.Infinity);
		controller.eyeRotator.localRotation = Quaternion.Slerp(controller.eyeRotator.localRotation, Quaternion.Euler (0, Mathf.Sin (controller.stateTimeElapsed*controller.stats.patrolRotationSpeed) * controller.stats.patrolAngle, 0), 0.5f);
	}
}