using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AI/StartWalking")]
    public class StartWalking : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.aiProgress.SetRandomFlyingKick();
            characterState.characterControl.aiController.WalkStraightToStartSphere();
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                return;
            }
            //Jump AI
            if (characterState.characterControl.aiProgress.EndSphereIsHigher())
            {
                if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() < 0.08f)
                {
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveLeft = false;
                    animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.jump_platform], true);

                    if (characterState.ROTATION_DATA.IsFacingForward())
                    {
                        characterState.characterControl.MoveRight = true;
                        characterState.characterControl.MoveLeft = false;
                    }
                    else if (!characterState.ROTATION_DATA.IsFacingForward())
                    {
                        characterState.characterControl.MoveRight = true;
                        characterState.characterControl.MoveLeft = false;
                    }

                    return;
                }
            }

            //Fall AI
            if (characterState.characterControl.aiProgress.EndSphereIsLower())
            {
                characterState.characterControl.aiController.WalkStraightToEndSphere();

                animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.fall_platform], true);
                return;
            }

            //Going straigh AI
            if (characterState.characterControl.aiProgress.AIDistanceToStartSphere() > 1.5f)
            {
                characterState.characterControl.Turbo = true;
            } else
            {
                characterState.characterControl.Turbo = false;
            }

            characterState.characterControl.aiController.WalkStraightToStartSphere();

            if (characterState.characterControl.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveRight = false;
            }
            if (characterState.characterControl.aiProgress.TargetIsOnTheSamePlatform())
            {
                characterState.characterControl.aiProgress.RepositionDestination();
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.jump_platform], false);
            animator.SetBool(HashManager.Instance.DicAIParams[AI_Walk_Transitions.fall_platform], false);
        }
    }
}
