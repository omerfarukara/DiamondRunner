using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    public void Run(Animator playerAnimator,bool status)
    {
        playerAnimator.SetBool(Constants.AnimationsTag.RUN, status);
    }

    public void Victory(Animator playerAnimator, bool status)
    {
        playerAnimator.SetBool(Constants.AnimationsTag.VICTORY, status);
    }

    public void BackwardJump(Animator playerAnimator)
    {
        playerAnimator.SetTrigger(Constants.AnimationsTag.BACKWARD_JUMP);
    }

    public void Dying(Animator playerAnimator, bool status)
    {
        playerAnimator.SetBool(Constants.AnimationsTag.DEAD,status);
    }
}
