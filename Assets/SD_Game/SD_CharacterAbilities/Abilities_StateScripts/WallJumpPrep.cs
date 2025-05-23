﻿using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/WallJumpPrep")]
    public class WallJumpPrep : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.MoveLeft = false;
            characterState.characterControl.MoveRight = false;
            characterState.characterControl.MOMENTUM_DATA.Momentum = 0f;
            characterState.characterControl.RIGID_BODY.velocity = Vector3.zero;

            if (characterState.ROTATION_DATA.IsFacingForward())
            {
                characterState.ROTATION_DATA.FaceForward(false);
            }
            else
            {
                characterState.ROTATION_DATA.FaceForward(true);
            }

            characterState.characterControl.LEDGE_GRAB_DATA.isGrabbingledge = false;
            characterState.characterControl.RIGID_BODY.useGravity = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.LEDGE_GRAB_DATA.isGrabbingledge = false;
            characterState.characterControl.RIGID_BODY.useGravity = true;
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

