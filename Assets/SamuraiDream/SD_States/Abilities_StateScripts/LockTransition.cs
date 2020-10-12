using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/LockTransition")]
    public class LockTransition : StateData
    {
        [Range(0f, 1f)]
        public float UnlockTime;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.animationProgress.LockTransition = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > UnlockTime)
            {
                characterState.characterControl.animationProgress.LockTransition = false;
            }
            else
            {
                characterState.characterControl.animationProgress.LockTransition = true;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}