using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdolling : MonoBehaviour
{
    public GameObject animatorToDisable;
    public RuntimeAnimatorController switchToTHisANimator;


    public void disableAnimator()
    {
        animatorToDisable.GetComponent<Animator>().enabled = false;




    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


}
