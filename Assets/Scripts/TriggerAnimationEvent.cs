using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationEvent : StateMachineBehaviour
{
    public float delay = 0;
    private float initialOffset = 0;
    public string eventName = "Done";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        initialOffset = stateInfo.normalizedTime * stateInfo.length;
        if (delay <= 0) Run(animator);
    }

    private bool called = false;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var time = stateInfo.normalizedTime * stateInfo.length - initialOffset;
        time *= stateInfo.speed * stateInfo.speedMultiplier * animator.speed;
        if (time > delay) Run(animator);
    }

    private void Run(Animator animator)
    {
        if (called) return;
        called = true;
        var comps = animator.gameObject.GetComponents<HandleAnimationEvent>();
        foreach (var comp in comps) {
            if (comp.eventName == eventName && !comp.wasCalled) {
                comp.OnDone?.Invoke();
                break;
            }
        }
    }
}
