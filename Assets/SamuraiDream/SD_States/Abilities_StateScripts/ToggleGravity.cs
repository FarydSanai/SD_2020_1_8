using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/AbilityData/ToggleGravity")]
    public class ToggleGravity : StateData
    {
        public bool OnStart;
        public bool On;
        public bool OnEnd;
        public float CustomTiming;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                ToggleGrav(characterState.characterControl);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (CustomTiming <= stateInfo.normalizedTime)
            {
                ToggleGrav(characterState.characterControl);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void ToggleGrav(CharacterController control)
        {
            control.RIGID_BODY.useGravity = On;
            control.RIGID_BODY.velocity = Vector3.zero;
        }
    }
}
