using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDone : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var comp = animator.gameObject.GetComponent<OnAnimatorDone>();
        comp?.OnDone?.Invoke();
    }

}
