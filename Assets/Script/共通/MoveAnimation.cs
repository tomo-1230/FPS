using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    public Animator MoveAnimationControl(MoveDate TargetData, Animator animator)
    {
        MoveDate data = TargetData;
        Animator anim = animator;
        if (data.Right)
        {
            anim.SetInteger("Right", 1);
        }
        else if (data.Left)
        {
            anim.SetInteger("Right", -1);
        }
        else
        {
            anim.SetInteger("Right", 0);
        }
        if (data.Forward)
        {
            anim.SetInteger("Forward", 1);
            if (data.Run)
            {
                anim.SetFloat("Blend", 1f);
                // anim.SetInteger("Right", 0);
            }
            else
            {
                anim.SetFloat("Blend", 0.5f);
            }
        }
        else if (data.Back)
        {
            anim.SetInteger("Forward", 1);
        }
        else
        {
            anim.SetInteger("Forward", 0);
        }
        if (data.Shot)
        {
            anim.SetBool("Have", true);
            anim.SetLayerWeight(2, 1f);
        }
        else
        {
            anim.SetBool("Have", false);
            anim.SetLayerWeight(2, 0f);
        }

        return anim;
    }
}
