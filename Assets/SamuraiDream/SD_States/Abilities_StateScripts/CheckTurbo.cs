using SamuraiGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamuraiDream/AbilityData/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        public bool MustRequireMovement;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Turbo)
            {
                if (MustRequireMovement)
                {
                    if (characterState.characterControl.MoveLeft || characterState.characterControl.MoveRight)
                    {
                        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
                    } else
                    {
                        animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
                    }
                } else
                {
                    animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], true);
                }                
            } else
            {
                animator.SetBool(HashManager.Instance.DicMainParams[TransitionParameter.Turbo], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
