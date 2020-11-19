﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/CheckTurboAndMovement")]
    public class CheckTurboAndMovement : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if ((characterState.characterControl.MoveLeft
                 || characterState.characterControl.MoveRight)
                 && characterState.characterControl.Turbo)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
            } else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
