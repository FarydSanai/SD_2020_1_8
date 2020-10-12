using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SamuraiGame
{
    [CreateAssetMenu(fileName = "New state", menuName = "SamuraiDream/Death/TriggerRagdoll")]
    public class TriggerRagdoll : StateData
    {
        [Range(0f, 1f)]
        public float TriggerTiming;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= TriggerTiming)
            {
                characterState.RAGDOLL_DATA.RagdollTriggered = true;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
