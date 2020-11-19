using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/RunningSlide")]
    public class RunningSlide : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.UpIsBlocked], false);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= 0.5)
            {
                if (BlockingObj.UpIsBlocked(characterState.characterControl))
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.UpIsBlocked], true);
                }
                else
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.UpIsBlocked], false);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.UpIsBlocked], false);
        }
    }
}