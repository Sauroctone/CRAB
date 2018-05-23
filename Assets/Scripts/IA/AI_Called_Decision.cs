using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Called")]
public class AI_Called_Decision : AI_Decision {

	public override bool Decide(StateController controller)
	{
		return controller.levelManager.called;
	}

}
