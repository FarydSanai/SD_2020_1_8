using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ROTATION_DATA.IsFacingForward())
            {
                if (characterState.characterControl.MoveLeft)
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], true);
                }
            }
            if (!characterState.ROTATION_DATA.IsFacingForward())
            {
                if (characterState.characterControl.MoveRight)
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], true);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turn], false);
        }
    }
}
