﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/Idle")]
    public class Idle : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
            animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], false);

            //characterState.characterControl.RIGID_BODY.useGravity = true;
            characterState.ROTATION_DATA.LockEarlyTurn = false;
            characterState.ROTATION_DATA.LockDirectionNextState = false;
            characterState.BLOCKING_DATA.ClearFrontBlockingObjDic();

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.ROTATION_DATA.LockEarlyTurn = false;
            characterState.ROTATION_DATA.LockDirectionNextState = false;

            if (characterState.characterControl.Jump)
            {
                //if (!characterState.JUMP_DATA.Jumped)
                //{
                //    if (characterState.GROUND_DATA.Ground != null)
                //    {
                //        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Jump], true);
                //    }
                //}
            }
            else
            {
                if (!characterState.ANIMATION_DATA.IsRunning(typeof(Jump)))
                {
                    characterState.JUMP_DATA.Jumped = false;
                }
            }
            if (characterState.characterControl.MoveRight && characterState.characterControl.MoveLeft)
            {
                //do nothing
            } else if (characterState.characterControl.MoveRight)
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Move], true);
            } else if (characterState.characterControl.MoveLeft)
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
