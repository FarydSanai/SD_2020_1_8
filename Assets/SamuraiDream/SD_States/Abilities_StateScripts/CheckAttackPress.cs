using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/CheckAttackPress")]
    public class CheckAttackPress : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.Attack)
            {
                characterState.characterControl.SkinnedMeshAnimator.SetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Attack], true);
            }
            else
            {
                characterState.characterControl.SkinnedMeshAnimator.SetBool(
                    HashManager.Instance.DicMainParams[TransitionParameter.Attack], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}