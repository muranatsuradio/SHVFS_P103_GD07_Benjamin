using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehaviour : StateMachineBehaviour
{
    private static readonly int IsReload = Animator.StringToHash("IsReload");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsReload,false);
        animator.SetBool(IsAiming,false);
        animator.GetComponentInChildren<PistolRayRenderer>().SetLineRendererActive(false);
        PlayerInputSystem.Instance.CanPlayerInteract = false;
        PlayerInputSystem.Instance.CanPlayerMove = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerInputSystem.Instance.CanPlayerInteract = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
