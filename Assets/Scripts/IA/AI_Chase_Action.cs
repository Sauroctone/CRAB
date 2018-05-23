using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class AI_Chase_Action : AI_Action
{
	public override void Act(StateController controller)
	{
		Chase (controller);
	}

	private void Chase(StateController controller)
	{
		if (controller.pathTimer <= 0) 
		{
			controller.follower.InitMovement (controller.transform.position, controller.chaseTarget.position, controller.stats.chaseSpeed);
			controller.pathTimer = controller.stats.pathUpdateFrequency;
		}
		//controller.eyeRotator.rotation = Quaternion.Slerp(controller.eyeRotator.localRotation, Quaternion.Euler(controller.originRotation), 0.001f);
		Quaternion aiRotation =  controller.transform.rotation*controller.originRotation;
		controller.eyeRotator.rotation = Quaternion.Slerp(controller.eyeRotator.rotation, Quaternion.Euler (0, 0, 0)*aiRotation, 0.001f);

		controller.CheckIfCountDownElapsed (Mathf.Infinity);

		if (!controller.animator.GetBool ("Chasing")) {
			controller.animator.SetBool ("Swimming", false);
			controller.animator.SetBool ("Immobile", false);
			controller.animator.SetBool ("Chasing", true);
		}
	}
}