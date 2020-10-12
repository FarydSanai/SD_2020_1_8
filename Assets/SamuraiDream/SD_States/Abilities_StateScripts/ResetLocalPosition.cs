using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/ResetLocalPosition")]
    public class ResetLocalPosition : StateData
    {
        public bool OnStart;
        public bool OnEnd;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                //CharacterController control = characterState.GetCharacterController(animator);
                characterState.characterControl.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterState.characterControl.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                //CharacterController control = characterState.GetCharacterController(animator);
                characterState.characterControl.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterState.characterControl.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
