using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiGame
{
    public class CharacterState : StateMachineBehaviour
    {
        public CharacterController characterControl;
        [Space(3)]
        public List<StateData> ListAbilityData = new List<StateData>();

        public BlockingObjData BLOCKING_DATA => characterControl.subComponentProcessor.blockingData;
        public RagdollData RAGDOLL_DATA => characterControl.subComponentProcessor.ragdollData;
        public BoxColliderData BOX_COLLIDER_DATA => characterControl.subComponentProcessor.boxColliderData;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA => characterControl.subComponentProcessor.verticalVelocityData;
        public MomentumData MOMENTUM_DATA => characterControl.subComponentProcessor.momentumData;
        public RotationData ROTATION_DATA => characterControl.subComponentProcessor.rotationData;
        public JumpData JUMP_DATA => characterControl.subComponentProcessor.jumpData;
        public CollisionData COLLISION_DATA => characterControl.subComponentProcessor.collisionData;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (characterControl == null)
            {
                CharacterController control = animator.transform.root.GetComponent<CharacterController>();
                control.CacheCharacterControl(animator);
            }
            foreach (StateData d in ListAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);

                if (characterControl.animationProgress.CurrentRunningAbilities.ContainsKey(d))
                {
                    characterControl.animationProgress.CurrentRunningAbilities[d] += 1;
                } 
                else
                {
                    characterControl.animationProgress.CurrentRunningAbilities.Add(d, 1);
                }
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.UpdateAbility(this, animator, stateInfo);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in ListAbilityData)
            {
                d.OnExit(this, animator, stateInfo);

                if (characterControl.animationProgress.CurrentRunningAbilities.ContainsKey(d))
                {
                    characterControl.animationProgress.CurrentRunningAbilities[d] -= 1;

                    if (characterControl.animationProgress.CurrentRunningAbilities[d] <= 0)
                    {
                        characterControl.animationProgress.CurrentRunningAbilities.Remove(d);
                    }
                }
            }
        }
    }
}

