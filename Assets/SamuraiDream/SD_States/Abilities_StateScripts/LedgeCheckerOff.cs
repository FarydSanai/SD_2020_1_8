using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/LedgeCheckerOff")]
    public class LedgeCheckerOff : StateData
    {
        private LedgeChecker ledgeChecker;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ledgeChecker == null)
            {
                ledgeChecker = characterState.characterControl.GetComponentInChildren<LedgeChecker>();
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.LEDGE_GRAB_DATA.isGrabbingledge)
            {
                if (ledgeChecker.Collider1.CollidedObjects.Count == 0)
                {
                    characterState.characterControl.LEDGE_GRAB_DATA.isGrabbingledge = false;
                    characterState.characterControl.RIGID_BODY.useGravity = true;
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
             
        }
    }
}