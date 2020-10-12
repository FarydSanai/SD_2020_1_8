using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New Sate", menuName = "SamuraiDream/AbilityData/TurnOnRootMotion")]
    public class TurnOnRootMotion : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterController control = characterState.GetCharacterController(animator);
            characterState.characterControl.SkinnedMeshAnimator.applyRootMotion = true;

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterController control = characterState.GetCharacterController(animator);
            characterState.characterControl.SkinnedMeshAnimator.applyRootMotion = false;
        }
    }

}