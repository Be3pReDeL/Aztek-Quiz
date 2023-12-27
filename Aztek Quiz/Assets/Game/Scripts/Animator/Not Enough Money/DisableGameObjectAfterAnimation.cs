using UnityEngine;

public class DisableGameObjectAfterAnimation : StateMachineBehaviour {
    [OPS.Obfuscator.Attribute.DoNotRename]
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.SetActive(false);
    }
}
