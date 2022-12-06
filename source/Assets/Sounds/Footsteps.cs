using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<AudioManager>().playlooped("Walk");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindObjectOfType<AudioManager>().forcestop("Walk");
    }
}
