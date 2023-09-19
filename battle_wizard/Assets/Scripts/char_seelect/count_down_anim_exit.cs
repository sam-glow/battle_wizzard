using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count_down_anim_exit : StateMachineBehaviour
{
    public event Action onExit;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (onExit != null)
            onExit();
    }
}
