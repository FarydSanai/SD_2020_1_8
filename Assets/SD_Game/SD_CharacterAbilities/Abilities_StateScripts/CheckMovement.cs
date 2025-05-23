﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/CheckMovement")]
    public class CheckMovement : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }   
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterController control = characterState.GetCharacterController(animator);
            if (characterState.characterControl.MoveLeft || characterState.characterControl.MoveRight)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            } else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
