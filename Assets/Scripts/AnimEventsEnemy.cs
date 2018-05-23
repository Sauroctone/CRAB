using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventsEnemy : MonoBehaviour {
	public SoundManager_Enemy sM;

	public void AttackSound()
	{
		sM.Play(sM.attackSFX, 1f);
	}
}
