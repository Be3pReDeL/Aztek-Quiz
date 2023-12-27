using UnityEngine;

public class DisableGameObjectAfterAnimation : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SetActive(false);
    }
}
