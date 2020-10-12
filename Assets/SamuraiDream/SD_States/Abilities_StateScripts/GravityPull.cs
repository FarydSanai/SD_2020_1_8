using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/GravityPull")]
    public class GravityPull : StateData
    {
        public AnimationCurve Gravity;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterController control = characterState.GetCharacterController(animator);
            //characterState.characterControl.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterController control = characterState.GetCharacterController(animator);
            //characterState.characterControl.GravityMultiplier = 0f;
        }
    }
}
