using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamuraiDream/AI/AITriggerAttack")]
    public class AITriggerAttack : StateData
    {
        delegate void GroundAttack(CharacterController control);
        private List<GroundAttack> ListGroundAttacks = new List<GroundAttack>();
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ListGroundAttacks.Count == 0)
            {
                ListGroundAttacks.Add(NormalGroundAttack);
                ListGroundAttacks.Add(ForwardGroundAttack);
            }
            characterState.characterControl.aiProgress.SetRandomFlyingKick();
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.TargetIsDead())
            {
                characterState.characterControl.Attack = false;
                return;
            }
            if (characterState.characterControl.Turbo && characterState.characterControl.aiProgress.AIDistanceToTarget() < 3f)
            {
                FlyingKick(characterState.characterControl);
            }
            else if (!characterState.characterControl.Turbo && characterState.characterControl.aiProgress.AIDistanceToTarget() < 1f)
            {
                ListGroundAttacks[Random.Range(0, ListGroundAttacks.Count)](characterState.characterControl);
            } 
            else
            {
                characterState.characterControl.Attack = false;
            }

            characterState.characterControl.animationProgress.AttackTriggered = characterState.characterControl.Attack;
   
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public void NormalGroundAttack(CharacterController control)
        {
            if (control.aiProgress.TargetIsOnTheSamePlatform())
            {
                control.MoveLeft = false;
                control.MoveRight = false;
                if (control.aiProgress.IsFacingTarget() &&
                    !control.animationProgress.IsRunning(typeof(MoveForward)))
                {
                    control.Attack = true;
                }
            } else
            {
                control.Attack = false;
            }
        }
        public void ForwardGroundAttack(CharacterController control)
        {
            if (control.aiProgress.TargetIsOnTheSamePlatform())
            {
                if (control.aiProgress.TargetIsOnRightSide())
                {
                    control.MoveRight = true;
                    control.MoveLeft = false;
                    if (control.aiProgress.IsFacingTarget() &&
                        control.animationProgress.IsRunning(typeof(MoveForward)))
                    {
                        control.Attack = true;
                    }
                }
                else
                {
                    control.MoveRight = false;
                    control.MoveLeft = true;
                    if (control.aiProgress.IsFacingTarget() &&
                        control.animationProgress.IsRunning(typeof(MoveForward)))
                    {
                        control.Attack = true;
                    }
                }
            } 
            else
            {
                control.Attack = false;
            }
        }
        public void FlyingKick(CharacterController control)
        {
            if (control.aiProgress.DoFlyingKick
                && control.aiProgress.TargetIsOnTheSamePlatform())
            {
                control.Attack = true;
            }
            else
            {
                control.Attack = false;
            }
        }
    }
}
