using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/SwitchAnimator")]
    public class SwitchAnimator : CharacterAbility
    {
        public float SwitchTiming;
        public RuntimeAnimatorController TargetAnimatorController;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.characterControl.RIGID_BODY.useGravity = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= SwitchTiming)
            {
                characterState.characterControl.SkinnedMeshAnimator.runtimeAnimatorController = TargetAnimatorController;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}