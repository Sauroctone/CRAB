using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnimationEvents : MonoBehaviour {

    public SoundManager soundMan;
    public GameObject footstepCloud;
    public Transform[] legs;
    public SeaWeedManager sW;

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

    void OnSnapLeft()
    {
        bool hasSnipped = sW.OnClaw(PlayerController.Claw.Left);

        if (hasSnipped)
        {
            soundMan.Play(soundMan.pincerAlgae, 1f, 0.95f, 1.05f, -0.1f);
        }

        else
        {
            soundMan.Play(soundMan.pincer, 0.95f, 1f, 1.05f, -0.1f);
        }
    }

    void OnSnapRight()
    {
        bool hasSnipped = sW.OnClaw(PlayerController.Claw.Right);

        if (hasSnipped)
        {
            soundMan.Play(soundMan.pincerAlgae, 1f, 0.95f, 1.05f, 0.1f);
        }

        else
        {
            soundMan.Play(soundMan.pincer, 0.95f, 1f, 1.05f, 0.1f);
        }
    }
}
