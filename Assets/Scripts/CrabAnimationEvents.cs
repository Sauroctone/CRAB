using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnimationEvents : MonoBehaviour {

    public SoundManager soundMan;
    public GameObject footstepCloud;
    public Transform[] legs;

	void OnLegDown()
    {
        soundMan.Play(soundMan.footSteps[Random.Range(0, soundMan.footSteps.Length)], .3f, 0.95f, 1.05f);
        Instantiate(footstepCloud, legs[1].position, Quaternion.identity);
        Instantiate(footstepCloud, legs[4].position, Quaternion.identity);
    }

    void OnLegsDown()
    {
        soundMan.Play(soundMan.footSteps[Random.Range(0, soundMan.footSteps.Length)], .3f, 0.95f, 1.05f);
        Instantiate(footstepCloud, legs[0].position, Quaternion.identity);
        Instantiate(footstepCloud, legs[2].position, Quaternion.identity);
        Instantiate(footstepCloud, legs[3].position, Quaternion.identity);
        Instantiate(footstepCloud, legs[5].position, Quaternion.identity);
    }
}
