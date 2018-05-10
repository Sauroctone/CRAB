using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Attack")]
public class AI_Attack_Decision : AI_Decision {
	public LayerMask layer;
	public override bool Decide(StateController controller)
	{
		bool canAttack = Attack(controller);
		if (canAttack)
			InitAttack (controller);
		
		return canAttack;
	}

	private bool Attack(StateController controller)
	{
		Vector3 mouthPosition = controller.mouth.position;
		bool closeEnough = false;
		if ((controller.mouth.position - controller.chaseTarget.position).magnitude <= controller.stats.attackRadius) 
		{
			closeEnough = true;
		}

		return closeEnough && controller.CheckIfCountDownElapsed (controller.stats.attackCooldown);
	}

	private void InitAttack(StateController controller)
	{
		controller.attackTargetPosition = controller.chaseTarget.position;
		controller.follower.Attack ();
	}
}
