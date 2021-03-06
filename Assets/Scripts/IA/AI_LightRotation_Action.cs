﻿using System.Collections;
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
		//controller.eyeRotator.localRotation = Quaternion.Slerp(controller.eyeRotator.localRotation, Quaternion.Euler (controller.originLocalRotation.x  + Mathf.Sin (controller.stateTimeElapsed*controller.stats.patrolRotationSpeed) * controller.stats.patrolAngle, controller.originLocalRotation.y, controller.originLocalRotation.z), 0.5f);
		//controller.eyeRotator.localRotation = Quaternion.Slerp(controller.eyeRotator.localRotation, Quaternion.Euler (0, Mathf.Sin (controller.stateTimeElapsed*controller.stats.patrolRotationSpeed) * controller.stats.patrolAngle, 0), 0.5f);
		Quaternion aiRotation = controller.transform.rotation*controller.originRotation;
		controller.eyeRotator.rotation = Quaternion.Slerp(controller.eyeRotator.rotation, Quaternion.Euler (0, Mathf.Sin (controller.stateTimeElapsed*controller.stats.patrolRotationSpeed) * controller.stats.patrolAngle, 0) * aiRotation, 0.5f);

	}
}