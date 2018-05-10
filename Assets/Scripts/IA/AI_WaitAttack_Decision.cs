using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/WaitAttack")]
public class AI_WaitAttack_Decision : AI_Decision {

	public override bool Decide(StateController controller)
	{
		bool attackOver = Wait(controller);
		return attackOver;
	}

	private bool Wait(StateController controller)
	{
		return controller.CheckIfCountDownElapsed(controller.stats.attackTime);
	}
}
